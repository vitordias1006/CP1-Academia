using CP1_Academia.Domain.Entities;

namespace CP1_Academia.API.Application.DTOs;

public record UnidadeAcademiaResponse(
    Guid Id,
    string Telefone,
    bool Ativo,
    DateTime HorarioFuncionamento,
    Guid RedeAcademiaId,
    Guid GerenteId,
    Guid LocalizacaoId)
{
    public static UnidadeAcademiaResponse FromDomain(UnidadeAcademia unidadeAcademia) => new(unidadeAcademia.Id, unidadeAcademia.Telefone, unidadeAcademia.Ativo, unidadeAcademia.HorarioFuncionamento, unidadeAcademia.RedeAcademiaId, unidadeAcademia.GerenteId, unidadeAcademia.LocalizacaoId);
}