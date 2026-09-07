using System;
using CP1_Academia.Domain.Entities;
using CP1_Academia.Domain.Exceptions;
using Xunit;

namespace CP1_Academia.Domain.Tests;

public class RedeAcademiaTests
{
    [Fact]
    public void Construtor_DadosValidos_CriaRedeAcademiaComSucesso()
    {
        var rede = new RedeAcademia("PowerFit", 5, "12345678000199", DateTime.Now.AddYears(-3));

        Assert.Equal("PowerFit", rede.Nome);
        Assert.Equal(5, rede.QntdUnidades);
    }

    [Theory]
    [InlineData(null, "12345678000199", 5)]
    [InlineData("", "12345678000199", 5)]
    [InlineData("PowerFit", null, 5)]
    [InlineData("PowerFit", "", 5)]
    [InlineData("PowerFit", "12345678000199", -1)]
    public void Construtor_DadosInvalidos_LancaDomainException(string nome, string cnpj, int qntdUnidades)
    {
        void Act() => new RedeAcademia(nome, qntdUnidades, cnpj, DateTime.Now);

        Assert.Throws<DomainException>(Act);
    }
}