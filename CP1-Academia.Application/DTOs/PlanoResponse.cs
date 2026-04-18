using CP1_Academia.Domain.Entities;

namespace CP1_Academia.API.Application.DTOs;

public record PlanoResponse(
    double Preco,
    DateTime DataDeAssinatura,
    DateTime DataDeRenovacao,
    string TipoPlano,
    bool Fidelidade,
    bool Ativo)
{
   public static PlanoResponse FromDomain(Plano plano) 
       => new (plano.Preco, plano.DataDeAssinatura,  plano.DataDeRenovacao, plano.TipoPlano, plano.Fidelidade, plano.Ativo);
}