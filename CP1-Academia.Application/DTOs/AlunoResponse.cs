using CP1_Academia.Domain.Entities;

namespace CP1_Academia.API.Application.DTOs;

public record AlunoResponse(
    Guid Id,
    string Nome,
    string Cpf,
    string Email,
    string Telefone,
    DateTime DataMatricula,
    bool Ativo,
    Guid PlanoId)
{
    public static AlunoResponse FromDomain(Aluno aluno) => new (aluno.Id, aluno.Nome, aluno.Cpf, aluno.Email, aluno.Telefone, aluno.DataMatricula, aluno.Ativo, aluno.PlanoId);
}