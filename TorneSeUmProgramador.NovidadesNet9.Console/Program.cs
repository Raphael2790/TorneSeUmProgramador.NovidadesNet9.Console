// See https://aka.ms/new-console-template for more information
using System.Diagnostics.CodeAnalysis;
using AlunoEntidade = TorneSeUmProgramador.NovidadesNet9.Console.Entidade.Aluno;
using Dto = TorneSeUmProgramador.NovidadesNet9.Console.Dto;
using Point = (int x, int y);
using Aluno = (string Nome, int Idade);
using Z = int?;
using Dicionario = System.Collections.Generic.Dictionary<string, string>;

//Console.WriteLine("Hello, World!");

////expressões de coleção e operador spread
//List<string> b = ["one", "two", "three"];
//int[] numeros = [1, 2, 3, 4, 5];
//List<string> c = [];
//ICollection<int> numero = [1, 2, 3, 4, 5];
//int[] numeros2 = [.. numeros, 6];
//c.AddRange(b);

////Array em linha ou Matriz embutida
//int[] numeros3 = { 1, 2, 3, 4, 5 };
//var salaDeAula = new SalaDeAula();
//for (int i = 0; i < 10; i++)
//{
//    salaDeAula[i] = i;
//}

//Parametros opcionais em lambdas
var soma = (int a, int b = 0) => a + b;
Console.WriteLine(soma(1, 2));
Console.WriteLine(soma(10));

//Ref read-only parameters
int Incrementar(ref readonly int x) => x + 1;

int x = 0;
int y = Incrementar(ref x);
Console.WriteLine($"{x},{y}");

//Atributos experimentais
[Experimental("FuncaoEmTeste")]
void FuncaoEmTeste()
{
    Console.WriteLine("Funcao Em Teste");
}

FuncaoEmTeste();

[Obsolete($"Utilizar metodo {nameof(MetodoNovo)}")]
void MetodoEmDesuso()
{
    Console.WriteLine("Metodo Em Desuso");
}

MetodoEmDesuso();

void MetodoNovo()
{
    Console.WriteLine("Metodo Novo");
}

//Alias as any type
var aluno = new AlunoEntidade();
var aluno2 = new Dto.Aluno();
Point p = (10, 20);
Aluno a = ("Rafael", 30);
Z z = 10;
Dicionario dicionario = new();

//range operator
int[] numeros = { 1, 2, 3, 4, 5 };
int[] subArray = numeros[1..^1];
Console.WriteLine(string.Join(",", subArray));