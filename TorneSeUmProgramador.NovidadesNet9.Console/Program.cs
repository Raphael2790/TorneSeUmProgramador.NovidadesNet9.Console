// See https://aka.ms/new-console-template for more information
using TorneSeUmProgramador.NovidadesNet9.Console;

Console.WriteLine("Hello, World!");

//expressões de coleção e operador spread
List<string> b = ["one", "two", "three"];
int[] numeros = [1, 2, 3, 4, 5];
List<string> c = [];
ICollection<int> numero = [1, 2, 3, 4, 5];
int[] numeros2 = [.. numeros, 6];
c.AddRange(b);

//Array em linha ou Matriz embutida
int[] numeros3 = { 1, 2, 3, 4, 5 };
var salaDeAula = new SalaDeAula();
for (int i = 0; i < 10; i++)
{
    salaDeAula[i] = i;
}