using CP1_Academia.Domain.Entities;
using CP1_Academia.Domain.Exceptions;
using Xunit;

namespace CP1_Academia.Domain.Tests;

public class PlanoTests
{
    [Fact]
    public void Construtor_DadosValidos_CriaPlanoComSucesso()
    {
        // Arrange
        var dataAssinatura = DateTime.Now;
        var dataRenovacao = dataAssinatura.AddMonths(12);

        // Act
        var plano = new Plano(150.00, dataAssinatura, dataRenovacao, "Mensal", true, true);

        // Assert
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
        // Arrange
        var dataAssinatura = DateTime.Now;
        var dataRenovacao = dataAssinatura.AddMonths(12);

        // Act
        void Act() => new Plano(preco, dataAssinatura, dataRenovacao, tipoPlano, true, true);

        // Assert
        Assert.Throws<DomainException>(Act);
    }

    [Fact]
    public void Construtor_DataRenovacaoAnteriorAssinatura_LancaDomainException()
    {
        // Arrange
        var dataAssinatura = DateTime.Now;
        var dataRenovacao = dataAssinatura.AddDays(-1);

        // Act
        void Act() => new Plano(150, dataAssinatura, dataRenovacao, "Mensal", true, true);

        // Assert
        Assert.Throws<DomainException>(Act);
    }
}