using CP1_Academia.Domain.Entities;
using CP1_Academia.Domain.Exceptions;
using Xunit;

namespace CP1_Academia.Domain.Tests;

public class InstrutorTests
{
    [Fact]
    public void Construtor_DadosValidos_CriaInstrutorComSucesso()
    {
        var instrutor = new Instrutor(
            "Carlos", "11122233344", "carlos@email.com", "Instrutor",
            Guid.NewGuid(), 3000, DateTime.Now, true, Guid.NewGuid(), "123456-G/SP");

        Assert.Equal("Carlos", instrutor.Nome);
        Assert.Equal("123456-G/SP", instrutor.Cref);
    }

    [Fact]
    public void Construtor_SalarioInvalido_LancaDomainException_HerdaValidacaoDeFuncionario()
    {
        void Act() => new Instrutor(
            "Carlos", "11122233344", "carlos@email.com", "Instrutor",
            Guid.NewGuid(), 0, DateTime.Now, true, Guid.NewGuid(), "123456-G/SP");

        Assert.Throws<DomainException>(Act);
    }
}