using System.ComponentModel.DataAnnotations;
using CP1_Academia.Domain.Entities;

namespace CP1_Academia.API.Application.DTOs;

public class PlanoRequest
{
    [Required(ErrorMessage = "O preço é obrigatório")]
    [Range(0.01, 99999.99, ErrorMessage = "O preço deve ser um valor positivo")]
    public double Preco { get; set; }

    [Required(ErrorMessage = "A data de assinatura é obrigatória")]
    public DateTime DataDeAssinatura { get; set; }

    [Required(ErrorMessage = "A data de renovação é obrigatória")]
    public DateTime DataDeRenovacao { get; set; }

    [Required(ErrorMessage = "O tipo de plano é obrigatório")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O tipo de plano deve ter entre 2 e 100 caracteres")]
    public string TipoPlano { get; set; }

    public bool Fidelidade { get; set; }

    public bool Ativo { get; set; }

    public Plano ToDomain() => new Plano(
        Preco,
        DataDeAssinatura,
        DataDeRenovacao,
        TipoPlano,
        Fidelidade,
        Ativo);
}