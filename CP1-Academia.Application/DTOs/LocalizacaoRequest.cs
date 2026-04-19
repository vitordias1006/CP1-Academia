using System.ComponentModel.DataAnnotations;
using CP1_Academia.Domain.Entities;

namespace CP1_Academia.API.Application.DTOs;

public record LocalizacaoRequest(
    [property: Required(ErrorMessage = "O estado é obrigatório")]
    [property: StringLength(2, MinimumLength = 2, ErrorMessage = "O estado deve ser a sigla com 2 caracteres (ex: SP)")]
    string Estado,

    [property: Required(ErrorMessage = "A cidade é obrigatória")]
    [property: StringLength(100, MinimumLength = 2, ErrorMessage = "A cidade deve ter entre 2 e 100 caracteres")]
    string Cidade,

    [property: Required(ErrorMessage = "O bairro é obrigatório")]
    [property: StringLength(100, MinimumLength = 2, ErrorMessage = "O bairro deve ter entre 2 e 100 caracteres")]
    string Bairro,

    [property: Required(ErrorMessage = "O CEP é obrigatório")]
    [property: RegularExpression(@"^\d{8}$", ErrorMessage = "O CEP deve conter exatamente 8 dígitos numéricos")]
    string Cep,

    [property: Required(ErrorMessage = "A rua é obrigatória")]
    [property: StringLength(200, MinimumLength = 2, ErrorMessage = "A rua deve ter entre 2 e 200 caracteres")]
    string Rua,

    [property: Required(ErrorMessage = "O número é obrigatório")]
    [property: Range(1, 99999, ErrorMessage = "O número deve estar entre 1 e 99999")]
    int Numero,

    [property: Required(ErrorMessage = "O identificador da unidade é obrigatório")]
    Guid UnidadeAcademiaId)
{
    public Localizacao ToDomain() => new(
        Estado,
        Cidade,
        Bairro,
        Cep,
        Rua,
        Numero,
        UnidadeAcademiaId);
}
