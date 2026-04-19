using CP1_Academia.Domain.Entities;

namespace CP1_Academia.API.Application.DTOs;

public record GerenteResponse(
    Guid Id,
    string Nome,
    string Cpf,
    string Email,
    string Cargo,
    Guid GerenteId,
    double Salario,
    DateTime DataDeContratacao,
    bool Ativo,
    Guid UnidadeAcademiaId,
    double Comissao,
    DateTime PeriodoDeLideranca,
    string AreaDeResponsabilidade,
    string NivelDeLideranca)
{
    public static GerenteResponse FromDomain(Gerente gerente) => new(gerente.Id, gerente.Nome, gerente.Cpf, gerente.Email, gerente.Cargo, gerente.GerenteId, gerente.Salario, gerente.DataDeContratacao, gerente.Ativo, gerente.UnidadeAcademiaId, gerente.Comissao, gerente.PeriodoDeLideranca, gerente.AreaDeResponsabilidade, gerente.NivelDeLideranca);
}