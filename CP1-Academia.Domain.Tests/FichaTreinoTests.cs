using CP1_Academia.Domain.Entities;
using CP1_Academia.Domain.Exceptions;
using Xunit;

namespace CP1_Academia.Domain.Tests;

public class FichaTreinoTests
{
    [Fact]
    public void Construtor_DadosValidos_CriaFichaTreinoComSucesso()
    {
        var alunoId = Guid.NewGuid();

        var ficha = new FichaTreino("Supino reto", 12, 4, "Força", "Peitoral", "Observação", alunoId);

        Assert.Equal("Supino reto", ficha.Exercicios);
        Assert.Equal(12, ficha.Repeticoes);
        Assert.Equal(4, ficha.Series);
    }

    [Theory]
    [InlineData(null, 12, 4)]
    [InlineData("", 12, 4)]
    [InlineData("Supino reto", 0, 4)]
    [InlineData("Supino reto", 12, 0)]
    public void Construtor_DadosInvalidos_LancaDomainException(string exercicios, int repeticoes, int series)
    {
        void Act() => new FichaTreino(exercicios, repeticoes, series, "Força", "Peitoral", "obs", Guid.NewGuid());

        Assert.Throws<DomainException>(Act);
    }
}