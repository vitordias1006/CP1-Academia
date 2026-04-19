using System.ComponentModel.DataAnnotations;
using CP1_Academia.Domain.Entities;

namespace CP1_Academia.API.Application.DTOs;

public record GerenteRequest(
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

    [property: Required(ErrorMessage = "O cargo é obrigatório")]
    [property: StringLength(100, MinimumLength = 2, ErrorMessage = "O cargo deve ter entre 2 e 100 caracteres")]
    string Cargo,

    [property: Required(ErrorMessage = "O identificador do gerente superior é obrigatório")]
    Guid GerenteId,

    [property: Required(ErrorMessage = "O salário é obrigatório")]
    [property: Range(0.01, 999999.99, ErrorMessage = "O salário deve ser um valor positivo")]
    double Salario,

    [property: Required(ErrorMessage = "A data de contratação é obrigatória")]
    [property:
        Range(typeof(DateTime), "1900-01-01", "2100-12-31",
            ErrorMessage = "A data de contratação deve estar entre 1900 e 2100")]
    DateTime DataDeContratacao,

    [property: Required(ErrorMessage = "O campo ativo é obrigatório")]
    bool Ativo,

    [property: Required(ErrorMessage = "O identificador da unidade é obrigatório")]
    Guid UnidadeAcademiaId,

    [property: Required(ErrorMessage = "A comissão é obrigatória")]
    [property: Range(0.0, 100.0, ErrorMessage = "A comissão deve estar entre 0 e 100")]
    double Comissao,

    [property: Required(ErrorMessage = "O período de liderança é obrigatório")]
    [property:
        Range(typeof(DateTime), "1900-01-01", "2100-12-31",
            ErrorMessage = "O período de liderança deve estar entre 1900 e 2100")]
    DateTime PeriodoDeLideranca,

    [property: Required(ErrorMessage = "A área de responsabilidade é obrigatória")]
    [property:
        StringLength(150, MinimumLength = 2,
            ErrorMessage = "A área de responsabilidade deve ter entre 2 e 150 caracteres")]
    string AreaDeResponsabilidade,

    [property: Required(ErrorMessage = "O nível de liderança é obrigatório")]
    [property:
        StringLength(50, MinimumLength = 2, ErrorMessage = "O nível de liderança deve ter entre 2 e 50 caracteres")]
    string NivelDeLideranca)
{
    public Gerente ToDomain() => new(
        Nome,
        Cpf,
        Email,
        Cargo,
        GerenteId,
        Salario,
        DataDeContratacao,
        Ativo,
        UnidadeAcademiaId,
        Comissao,
        PeriodoDeLideranca,
        AreaDeResponsabilidade,
        NivelDeLideranca);
}
