using ClassLibrary1.Entities;

namespace CP1_Application.DTOs;

public record AlunoResponse(
    string Nome,
    string Cpf,
    string Email,
    string Telefone,
    DateTime DataMatricula,
    bool Ativo,
    Guid PlanoId)
{
    public static AlunoResponse FromDomain(Aluno aluno) => new (aluno.Nome, aluno.Cpf, aluno.Email, aluno.Telefone, aluno.DataMatricula, aluno.Ativo, aluno.PlanoId);
}