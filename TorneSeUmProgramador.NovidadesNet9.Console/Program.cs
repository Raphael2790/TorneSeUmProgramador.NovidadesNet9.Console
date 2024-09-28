// See https://aka.ms/new-console-template for more information
using System.Diagnostics.CodeAnalysis;
using AlunoEntidade = TorneSeUmProgramador.NovidadesNet9.Console.Entidade.Aluno;
using Dto = TorneSeUmProgramador.NovidadesNet9.Console.Dto;
using Point = (int x, int y);
using Aluno = (string Nome, int Idade);
using Z = int?;
using Dicionario = System.Collections.Generic.Dictionary<string, string>;
using System.ComponentModel.DataAnnotations;

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
var senha = "123456";

var bytes = System.Text.Encoding.UTF8.GetBytes(senha);
var base64 = Convert.ToBase64String(bytes);

aluno2.HashSenha = base64+"quauaha";

var validationResults = new List<ValidationResult>();
var validationContext = new ValidationContext(aluno2, serviceProvider: null, items: null);

bool valido = Validator.TryValidateObject(aluno2, validationContext, validationResults, true);

//range operator
int[] numeros = { 1, 2, 3, 4, 5 };
int[] subArray = numeros[1..^1];
Console.WriteLine(string.Join(",", subArray));


//Utf8 Literals
//string s1 = "hello"u8;             // Error
//var s2 = "hello"u8;                // Okay and type is ReadOnlySpan<byte>
//ReadOnlySpan<byte> s3 = "hello"u8; // Okay.
//byte[] s4 = "hello"u8;             // Error - Cannot implicitly convert type 'System.ReadOnlySpan<byte>' to 'byte[]'.
//byte[] s5 = "hello"u8.ToArray();   // Okay.
//Span<byte> s6 = "hello"u8;         // Error - Cannot implicitly convert type 'System.ReadOnlySpan<byte>' to 'System.Span<byte>'.

async IAsyncEnumerable<string> StreamDataAsync()
{
    // Simula a produção de dados assíncrona
    for (var i = 0; i < 10; i++)
    {
        await Task.Delay(1000); // Simula uma operação assíncrona (ex: leitura de banco de dados)
        yield return $"Processado linha {i}. data: {DateTime.Now}";
    }
}

await foreach (var data in StreamDataAsync())
{
    Console.WriteLine(data);
}