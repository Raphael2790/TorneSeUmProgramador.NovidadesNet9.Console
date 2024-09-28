using System.ComponentModel.DataAnnotations;

namespace TorneSeUmProgramador.NovidadesNet9.Console.Dto;

public class Aluno
{
    [Required(ErrorMessage = "O nome precisa ser preenchido")]
    public string Nome { get; set; }

    [Range(1, 200)]
    [DeniedValues(0)]
    public int Idade { get; set; }

    [EmailAddress]
    [Length(100, 200, ErrorMessage = "Quantidade de caracteres email invalida")]
    public string Email { get; set; }

    [MaxLength(2)]
    [AllowedValues("SP", "RJ", "AM", "MG")]
    public string Regiao { get; set; }

    [Base64String]
    public string HashSenha { get; set; }
}
