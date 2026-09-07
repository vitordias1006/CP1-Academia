using CP1_Academia.Domain.Entities;
using CP1_Academia.Domain.Exceptions;

namespace CP1_Academia.Domain.Tests;

public class AulaExtraTests
{
    [Fact]
    public void Construtor_DadosValidos_CriaAulaExtraComSucesso()
    {
        var tipoDeAula = "Spinning";
        var horario = DateTime.Now.AddHours(1);
        var capacidade = 20;
        var fichaTreinoId = Guid.NewGuid();

        var aulaExtra = new AulaExtra(tipoDeAula, horario, capacidade, fichaTreinoId);

        Assert.Equal(tipoDeAula, aulaExtra.TipoDeAula);
        Assert.Equal(capacidade, aulaExtra.Capacidade);
    }

    [Theory]
    [InlineData(null, 10)]
    [InlineData("", 10)]
    [InlineData("Spinning", 0)]
    [InlineData("Spinning", -5)]
    public void Construtor_DadosInvalidos_LancaDomainException(string tipoDeAula, int capacidade)
    {
        var horario = DateTime.Now.AddHours(1);
        var fichaTreinoId = Guid.NewGuid();

        void Act() => new AulaExtra(tipoDeAula, horario, capacidade, fichaTreinoId);

        Assert.Throws<DomainException>(Act);
    }
}