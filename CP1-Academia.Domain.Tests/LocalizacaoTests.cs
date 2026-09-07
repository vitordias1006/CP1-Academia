using CP1_Academia.Domain.Entities;
using CP1_Academia.Domain.Exceptions;
using Xunit;

namespace CP1_Academia.Domain.Tests;

public class LocalizacaoTests
{
    [Fact]
    public void Construtor_DadosValidos_CriaLocalizacaoComSucesso()
    {
        var localizacao = new Localizacao("SP", "São Paulo", "Centro", "01000-000", "Rua A", 100, Guid.NewGuid());

        Assert.Equal("SP", localizacao.Estado);
        Assert.Equal("01000-000", localizacao.Cep);
    }

    [Theory]
    [InlineData(null, "01000-000")]
    [InlineData("", "01000-000")]
    [InlineData("SP", null)]
    [InlineData("SP", "")]
    public void Construtor_DadosInvalidos_LancaDomainException(string estado, string cep)
    {
        void Act() => new Localizacao(estado, "São Paulo", "Centro", cep, "Rua A", 100, Guid.NewGuid());

        Assert.Throws<DomainException>(Act);
    }
}