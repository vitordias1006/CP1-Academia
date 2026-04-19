using CP1_Academia.Domain.Entities;

namespace CP1_Academia.API.Application.DTOs;

public record FichaTreinoResponse(
    Guid Id,
    string Exercicios,
    int Repeticoes,
    int Series,
    string TipoExercicio,
    string MusculoAlvo,
    string Observacao,
    Guid AlunoId)
{
    public static FichaTreinoResponse FromDomain(FichaTreino fichaTreino) => new(fichaTreino.Id, fichaTreino.Exercicios, fichaTreino.Repeticoes, fichaTreino.Series, fichaTreino.TipoExercicio, fichaTreino.MusculoAlvo, fichaTreino.Observacao, fichaTreino.AlunoId);
}