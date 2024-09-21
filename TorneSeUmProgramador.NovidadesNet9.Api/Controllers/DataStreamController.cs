using Microsoft.AspNetCore.Mvc;

namespace TorneSeUmProgramador.NovidadesNet8.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DataStreamController : ControllerBase
{
    [HttpGet("stream")]
    public async IAsyncEnumerable<string> StreamDataAsync()
    {
        // Simula a produção de dados assíncrona
        for (var i = 0; i < 10; i++)
        {
            await Task.Delay(1000); // Simula uma operação assíncrona (ex: leitura de banco de dados)
            yield return $"Item {i} at {DateTime.Now}";
        }
    }
}
