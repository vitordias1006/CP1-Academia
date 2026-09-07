using CP1_Academia.Domain.Entities;
using CP1_Academia.Domain.Exceptions;
using Xunit;

namespace CP1_Academia.Domain.Tests;

public class UnidadeAcademiaTests
{
    [Fact]
    public void Construtor_DadosValidos_CriaUnidadeAcademiaComSucesso()
    {
        var unidade = new UnidadeAcademia("1133334444", true, DateTime.Now, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

        Assert.Equal("1133334444", unidade.Telefone);
        Assert.True(unidade.Ativo);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Construtor_TelefoneInvalido_LancaDomainException(string telefone)
    {
        void Act() => new UnidadeAcademia(telefone, true, DateTime.Now, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

        Assert.Throws<DomainException>(Act);
    }
}