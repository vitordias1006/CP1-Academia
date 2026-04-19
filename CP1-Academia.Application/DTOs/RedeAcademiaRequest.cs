using System.ComponentModel.DataAnnotations;
using CP1_Academia.Domain.Entities;

namespace CP1_Academia.API.Application.DTOs;

public record RedeAcademiaRequest(
    [property: Required(ErrorMessage = "O nome é obrigatório")]
    [property: StringLength(200, MinimumLength = 2, ErrorMessage = "O nome deve ter entre 2 e 200 caracteres")]
    string Nome,

    [property: Required(ErrorMessage = "A quantidade de unidades é obrigatória")]
    [property: Range(1, 10000, ErrorMessage = "A quantidade de unidades deve estar entre 1 e 10000")]
    int QntdUnidades,

    [property: Required(ErrorMessage = "O CNPJ é obrigatório")]
    [property: RegularExpression(@"^\d{14}$", ErrorMessage = "O CNPJ deve conter exatamente 14 dígitos numéricos")]
    string Cnpj,

    [property: Required(ErrorMessage = "A data de fundação é obrigatória")]
    [property:
        Range(typeof(DateTime), "1900-01-01", "2100-12-31",
            ErrorMessage = "A data de fundação deve estar entre 1900 e 2100")]
    DateTime DataFundacao)
{
    public RedeAcademia ToDomain() => new(
        Nome, 
        QntdUnidades, 
        Cnpj,
        DataFundacao);
}
