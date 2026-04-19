using CP1_Academia.Domain.Entities;

namespace CP1_Academia.API.Application.DTOs;

public record AulaExtraResponse(
    Guid Id,
    string TipoDeAula,
    DateTime HorarioAula,
    int Capacidade,
    Guid FichaTreinoId)
{
    public static AulaExtraResponse FromDomain(AulaExtra aulaExtra) => new(aulaExtra.Id, aulaExtra.TipoDeAula, aulaExtra.HorarioAula, aulaExtra.Capacidade, aulaExtra.FichaTreinoId);
}