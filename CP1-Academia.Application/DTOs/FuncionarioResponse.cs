using CP1_Academia.Domain.Entities;

namespace CP1_Academia.API.Application.DTOs;

public record FuncionarioResponse(
    Guid Id,
    string Nome,
    string Cpf,
    string Email,
    string Cargo,
    Guid GerenteId,
    double Salario,
    DateTime DataDeContratacao,
    bool Ativo,
    Guid UnidadeAcademiaId)
{
    public static FuncionarioResponse FromDomain(Funcionario funcionario) => new(funcionario.Id, funcionario.Nome, funcionario.Cpf, funcionario.Email, funcionario.Cargo, funcionario.GerenteId, funcionario.Salario, funcionario.DataDeContratacao, funcionario.Ativo, funcionario.UnidadeAcademiaId);
}