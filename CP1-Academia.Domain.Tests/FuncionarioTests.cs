using CP1_Academia.Domain.Entities;
using CP1_Academia.Domain.Exceptions;
using Xunit;

namespace CP1_Academia.Domain.Tests;

public class FuncionarioTests
{
    [Fact]
    public void Construtor_DadosValidos_CriaFuncionarioComSucesso()
    {
        var gerenteId = Guid.NewGuid();
        var unidadeId = Guid.NewGuid();
        
        var funcionario = new Funcionario("João", "12345678900", "joao@email.com", "Recepção", gerenteId, 2500, DateTime.Now, true, unidadeId);

        Assert.Equal("João", funcionario.Nome);
        Assert.Equal(2500, funcionario.Salario);
        Assert.True(funcionario.Ativo);
    }

    [Theory]
    [InlineData(null, "12345678900", 2500)]
    [InlineData("", "12345678900", 2500)]
    [InlineData("João", null, 2500)]
    [InlineData("João", "12345678900", 0)]
    [InlineData("João", "12345678900", -100)]
    public void Construtor_DadosInvalidos_LancaDomainException(string nome, string cpf, double salario)
    {
        void Act() => new Funcionario(nome, cpf, "j@j.com", "Recepção", Guid.NewGuid(), salario, DateTime.Now, true, Guid.NewGuid());

        Assert.Throws<DomainException>(Act);
    }
}