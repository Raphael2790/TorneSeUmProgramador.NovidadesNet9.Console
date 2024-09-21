using TorneSeUmProgramador.NovidadesNet8.Api.UseCases.Interfaces;

namespace TorneSeUmProgramador.NovidadesNet8.Api.UseCases;

public class EditarWeatherForecastUseCase : IUseCase
{
    public async Task ExecuteAsync(WeatherForecast forecast)
    {
        await Task.CompletedTask;
    }
}
