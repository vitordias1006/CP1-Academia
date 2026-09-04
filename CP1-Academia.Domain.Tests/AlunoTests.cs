using CP1_Academia.Domain.Entities;
using CP1_Academia.Domain.Exceptions;
using Xunit;

namespace CP1_Academia.Domain.Tests;

public class AlunoTests
{
    [Fact]
    public void Construtor_DadosValidos_CriaAlunoComSucesso()
    {
        var nome = "Maria Silva";
        var cpf = "12345678900";
        var email = "maria@email.com";
        var telefone = "11999999999";
        var dataMatricula = DateTime.Now.AddDays(-1);
        var planoId = Guid.NewGuid();

        var aluno = new Aluno(nome, cpf, email, telefone, dataMatricula, ativo: true, planoId);

        Assert.Equal(nome, aluno.Nome);
        Assert.Equal(cpf, aluno.Cpf);
        Assert.True(aluno.Ativo);
        Assert.Equal(planoId, aluno.PlanoId);
    }

    [Theory]
    [InlineData(null, "12345678900")]
    [InlineData("", "12345678900")]
    [InlineData("   ", "12345678900")]
    [InlineData("Maria Silva", null)]
    [InlineData("Maria Silva", "")]
    public void Construtor_NomeOuCpfInvalido_LancaDomainException(string nome, string cpf)
    {
        var dataMatricula = DateTime.Now;
        var planoId = Guid.NewGuid();

        void Act() => new Aluno(nome, cpf, "a@a.com", "11999999999", dataMatricula, true, planoId);

        Assert.Throws<DomainException>(Act);
    }

    [Fact]
    public void Construtor_DataMatriculaNoFuturo_LancaDomainException()
    {
        var dataFutura = DateTime.Now.AddDays(10);

        void Act() => new Aluno("João", "98765432100", "j@j.com", "11888888888", dataFutura, true, Guid.NewGuid());

        Assert.Throws<DomainException>(Act);
    }
}