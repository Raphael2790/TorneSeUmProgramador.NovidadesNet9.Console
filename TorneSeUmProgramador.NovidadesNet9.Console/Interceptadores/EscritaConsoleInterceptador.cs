using System.Runtime.CompilerServices;

namespace TorneSeUmProgramador.NovidadesNet9.Console.Interceptadores;

public static class EscritaConsoleInterceptador
{
    [Interceptador(@"C:\Users\rsssh\source\repos\TorneSeUmProgramador.NovidadesNet9.Console\TorneSeUmProgramador.NovidadesNet9.Console\Program.cs", line: 33, character: 9)]
    public static void InterceptarConsoleWrite(string value)
    {
        System.Console.WriteLine(value);
    }
}
