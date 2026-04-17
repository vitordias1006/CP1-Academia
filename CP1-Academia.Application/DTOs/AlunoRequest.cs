using System.ComponentModel.DataAnnotations;
using CP1_Academia.API.Domain.Entities;

namespace CP1_Academia.API.Application.DTOs;

public record AlunoRequest(
    [property: Required(ErrorMessage = "O nome é obrigatório")]
    [property: StringLength(150, MinimumLength = 2, ErrorMessage = "O nome deve ter entre 2 e 150 caracteres")]
    string Nome,

    [property: Required(ErrorMessage = "O CPF é obrigatório")]
    [property: RegularExpression(@"^\d{11}$", ErrorMessage = "O CPF deve conter exatamente 11 dígitos numéricos")]
    string Cpf,

    [property: Required(ErrorMessage = "O e-mail é obrigatório")]
    [property: EmailAddress(ErrorMessage = "O e-mail informado não é válido")]
    [property: StringLength(200, ErrorMessage = "O e-mail deve ter no máximo 200 caracteres")]
    string Email,

    [property: Required(ErrorMessage = "O telefone é obrigatório")]
    [property:
        RegularExpression(@"^\d{10,11}$", ErrorMessage = "O telefone deve conter entre 10 e 11 dígitos numéricos")]
    string Telefone,

    [property: Required(ErrorMessage = "A data de matrícula é obrigatória")]
    [property:
        Range(typeof(DateTime), "2000-01-01", "2100-12-31",
            ErrorMessage = "A data de matrícula deve estar entre 2000 e 2100")]
    DateTime DataMatricula,

    [property: Required(ErrorMessage = "O campo ativo é obrigatório")]
    bool Ativo,

    [property: Required(ErrorMessage = "O identificador do plano é obrigatório")]
    Guid PlanoId)
{
    public Aluno ToDomain() => new Aluno(
        Nome,
        Cpf,
        Email, 
        Telefone, 
        DataMatricula, 
        Ativo,
        PlanoId);
}
