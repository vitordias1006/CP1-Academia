using System.ComponentModel.DataAnnotations;

namespace CP1_Academia.API.Application.DTOs;

public record PlanoRequest(
    [property: Required(ErrorMessage = "O preço é obrigatório")]
    [property: Range(0.01, 99999.99, ErrorMessage = "O preço deve ser um valor positivo")]
    double Preco,

    [property: Required(ErrorMessage = "A data de assinatura é obrigatória")]
    [property: Range(typeof(DateTime), "2000-01-01", "2100-12-31", ErrorMessage = "A data de assinatura deve estar entre 2000 e 2100")]
    DateTime DataDeAssinatura,

    [property: Required(ErrorMessage = "A data de renovação é obrigatória")]
    [property: Range(typeof(DateTime), "2000-01-01", "2100-12-31", ErrorMessage = "A data de renovação deve estar entre 2000 e 2100")]
    DateTime DataDeRenovacao,

    [property: Required(ErrorMessage = "O tipo de plano é obrigatório")]
    [property: StringLength(100, MinimumLength = 2, ErrorMessage = "O tipo de plano deve ter entre 2 e 100 caracteres")]
    string TipoPlano,

    [property: Required(ErrorMessage = "O campo fidelidade é obrigatório")]
    bool Fidelidade,

    [property: Required(ErrorMessage = "O campo ativo é obrigatório")]
    bool Ativo);
