using CP1_Academia.Domain.Entities;

namespace CP1_Academia.API.Application.DTOs;

public record InstrutorResponse(
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
    string Cref)
{
    public static InstrutorResponse FromDomain(Instrutor instrutor) => new(instrutor.Id, instrutor.Nome, instrutor.Cpf, instrutor.Email, instrutor.Cargo, instrutor.GerenteId, instrutor.Salario, instrutor.DataDeContratacao, instrutor.Ativo, instrutor.UnidadeAcademiaId, instrutor.Cref);
}