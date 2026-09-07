using CP1_Academia.Domain.Entities;
using CP1_Academia.Domain.Exceptions;
using Xunit;

namespace CP1_Academia.Domain.Tests;

public class PlanoTests
{
    [Fact]
    public void Construtor_DadosValidos_CriaPlanoComSucesso()
    {
        var dataAssinatura = DateTime.Now;
        var dataRenovacao = dataAssinatura.AddMonths(12);

        var plano = new Plano(150.00, dataAssinatura, dataRenovacao, "Mensal", true, true);

        Assert.Equal(150.00, plano.Preco);
        Assert.Equal("Mensal", plano.TipoPlano);
    }

    [Theory]
    [InlineData(0, "Mensal")]
    [InlineData(-10, "Mensal")]
    [InlineData(150, null)]
    [InlineData(150, "")]
    public void Construtor_DadosInvalidos_LancaDomainException(double preco, string tipoPlano)
    {
        var dataAssinatura = DateTime.Now;
        var dataRenovacao = dataAssinatura.AddMonths(12);

        void Act() => new Plano(preco, dataAssinatura, dataRenovacao, tipoPlano, true, true);

        Assert.Throws<DomainException>(Act);
    }

    [Fact]
    public void Construtor_DataRenovacaoAnteriorAssinatura_LancaDomainException()
    {
        var dataAssinatura = DateTime.Now;
        var dataRenovacao = dataAssinatura.AddDays(-1);

        void Act() => new Plano(150, dataAssinatura, dataRenovacao, "Mensal", true, true);

        Assert.Throws<DomainException>(Act);
    }
}