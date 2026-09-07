using CP1_Academia.Domain.Entities;
using CP1_Academia.Domain.Exceptions;
using Xunit;

namespace CP1_Academia.Domain.Tests;

public class GerenteTests
{
    [Fact]
    public void Construtor_DadosValidos_CriaGerenteComSucesso()
    {
        var gerente = new Gerente(
            "Ana", "98765432100", "ana@email.com", "Gerente", Guid.NewGuid(),
            5000, DateTime.Now, true, Guid.NewGuid(),
            comissao: 500, periodoDeLideranca: DateTime.Now, areaDeResponsabilidade: "Operações", nivelDeLideranca: "Sênior");

        Assert.Equal("Ana", gerente.Nome);
        Assert.Equal(500, gerente.Comissao);
    }

    [Fact]
    public void Construtor_NomeInvalido_LancaDomainException_HerdaValidacaoDeFuncionario()
    {
        void Act() => new Gerente(
            "", "98765432100", "ana@email.com", "Gerente", Guid.NewGuid(),
            5000, DateTime.Now, true, Guid.NewGuid(),
            500, DateTime.Now, "Operações", "Sênior");

        Assert.Throws<DomainException>(Act);
    }
}