using Microsoft.AspNetCore.RateLimiting;
using System.Collections.Frozen;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.RateLimiting;
using TorneSeUmProgramador.NovidadesNet8.Api.Middlewares;
using TorneSeUmProgramador.NovidadesNet8.Api.UseCases;
using TorneSeUmProgramador.NovidadesNet8.Api.UseCases.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ExceptionMiddleware>();
builder.Services.AddKeyedScoped<IUseCase, EditarWeatherForecastUseCase>("editar");
builder.Services.AddKeyedScoped<IUseCase, RegistrarWeatherForecastUseCase>("registrar");

builder.Services.AddRateLimiter(_ => _
    .AddFixedWindowLimiter(policyName: "fixed", options =>
    {
        options.PermitLimit = 4;
        options.Window = TimeSpan.FromSeconds(5);
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.QueueLimit = 2;
    }));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Configurações opcionais de serialização
        options.JsonSerializerOptions.DefaultBufferSize = 16 * 1024;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseRateLimiter();

app.MapControllers();

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateTime.Now.AddDays(index),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapPost("/weatherforecast", (WeatherForecast forecast, [FromKeyedServices("editar")] IUseCase registrar) =>
{
    registrar.ExecuteAsync(forecast);
    return Results.Created($"/weatherforecast/{forecast?.Date}", forecast);
})
 .WithName("PostWeatherForecast")
 .WithOpenApi();

app.MapGet("/random-items", () =>
{
    //Random shared veio para resolver o problema de concorrência
    var items = Enumerable.Range(1, 20).Select(index => Random.Shared.Next()).ToArray();
    var selecionados = Random.Shared.GetItems(items, 2);
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateTime.Now.AddDays(index),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    var forecastSelecionados = Random.Shared.GetItems(forecast, 2);

    return Results.Ok(new { selecionados, forecastSelecionados });
});

app.MapGet("/time-provider", () =>
{
    var timeStamp = TimeProvider.System.GetTimestamp();
    var date = TimeProvider.System.GetLocalNow();
    var timeSpan = TimeProvider.System.GetElapsedTime(timeStamp);

    return new { timeStamp, date, timeSpan };
})
 .WithName("GetTimeProvider")
 .WithOpenApi()
 .ShortCircuit();

static string GetTicks() => (DateTime.Now.Ticks & 0x11111).ToString("00000");

app.MapGet("/rate-limit", () => Results.Ok($"Hello {GetTicks()}"))
                           .WithName("Limit")
                           .WithOpenApi()
                           .RequireRateLimiting("fixed");

app.MapGet("derived-type", () =>
{
    var forecast = new WeatherForecastBase
    {
        Date = DateTimeOffset.Now,
        TemperatureCelsius = 25,
        Summary = "Hot"
    };

    WeatherForecastBase forecastWithCity = new WeatherForecastWithCity
    {
        Date = DateTimeOffset.Now,
        TemperatureCelsius = 25,
        Summary = "Hot",
        City = "São Paulo"
    };

    var jsonBase = JsonSerializer.Serialize(forecast);

    var jsonWithCity = JsonSerializer.Serialize(forecastWithCity);

    var forecastDeserialized = JsonSerializer.Deserialize<WeatherForecastBase>(jsonBase);

    var forecastWithCityDeserialized = JsonSerializer.Deserialize<WeatherForecastBase>(jsonWithCity);

    return Results.Ok(new { jsonBase, jsonWithCity });
});

app.MapGet("frozen-collections", () =>
{
    List<int> listaNormal = [1,2, 2, 3];
    ReadOnlyCollection<int> listaReadOnly = listaNormal.AsReadOnly();
    ImmutableList<int> listaImutavel = [.. listaNormal];


    //Net 8.0
    FrozenSet<int> frozenSet = listaNormal.ToFrozenSet();
    //FrozenDictionary<int, string> frozenDictionary = listaNormal.ToFrozenDictionary(x => x, x => x.ToString());

    listaImutavel = listaImutavel.Add(4);
    listaNormal.Add(4);

    return Results.Ok(new
    {
        normal = listaNormal.Count,
        leitura = listaReadOnly.Count,
        set = frozenSet.Count,
        imutavel = listaImutavel.Count,
        //dicionario = frozenDictionary.Count
    });
});

app.MapGet("regex-generator", (string texto) =>
{
    var regex = MyRegex();

    if (regex.IsMatch(texto))
    {
        return Results.Ok("Válido");
    }

    return Results.BadRequest("Inválido");
});

app.Run();

public record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

[JsonDerivedType(typeof(WeatherForecastBase), typeDiscriminator: "base")]
[JsonDerivedType(typeof(WeatherForecastWithCity), typeDiscriminator: "withCity")]
public class WeatherForecastBase
{
    public DateTimeOffset Date { get; set; }
    public int TemperatureCelsius { get; set; }
    public string? Summary { get; set; }
}

public class WeatherForecastWithCity : WeatherForecastBase
{
    public string? City { get; set; }
}

partial class Program
{
    [GeneratedRegex(@"\d{3}-\d{2}-\d{4}", RegexOptions.IgnoreCase)]
    private static partial Regex MyRegex();
}
