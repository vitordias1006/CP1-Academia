using CP1_Academia.API.Application.DTOs;
using CP1_Academia.API.Application.Services;
using CP1_Academia.Domain.Entities;
using CP1_Academia.Domain.Exceptions;
using CP1_Academia.Infrastructure;
using CP1_Academia.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace CP1_Academia.Application.Tests;

public class UnidadeAcademiaRepositoryTests
{
    private static AcademiaContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<AcademiaContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AcademiaContext(options);
    }

    private static (Mock<IRepository<RedeAcademia>> rede, Mock<IRepository<Gerente>> gerente, Mock<IRepository<Localizacao>> localizacao) CriarMocksValidos()
    {
        var rede = new Mock<IRepository<RedeAcademia>>();
        rede.Setup(r => r.ExistsById(It.IsAny<Guid>())).Returns(true);

        var gerente = new Mock<IRepository<Gerente>>();
        gerente.Setup(r => r.ExistsById(It.IsAny<Guid>())).Returns(true);

        var localizacao = new Mock<IRepository<Localizacao>>();
        localizacao.Setup(r => r.ExistsById(It.IsAny<Guid>())).Returns(true);

        return (rede, gerente, localizacao);
    }

    private static UnidadeAcademiaRequest CriarRequestValido(Guid redeId, Guid gerenteId, Guid localizacaoId) =>
        new("1133334444", true, DateTime.Now, redeId, gerenteId, localizacaoId);

    [Fact]
    public void Create_RedeAcademiaInexistente_LancaResourceNotFoundException_NaoPersiste()
    {
        var context = CreateInMemoryContext();
        var (rede, gerente, localizacao) = CriarMocksValidos();
        rede.Setup(r => r.ExistsById(It.IsAny<Guid>())).Returns(false);

        var sut = new UnidadeAcademiaRepository(context, rede.Object, gerente.Object, localizacao.Object);
        var request = CriarRequestValido(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

        void Act() => sut.Create(request);

        Assert.Throws<ResourceNotFoundException>(Act);
        Assert.Empty(context.UnidadeAcademias);
    }

    [Fact]
    public void Create_TodasDependenciasExistentes_PersisteUnidadeUmaVez()
    {
        var context = CreateInMemoryContext();
        var (rede, gerente, localizacao) = CriarMocksValidos();

        var sut = new UnidadeAcademiaRepository(context, rede.Object, gerente.Object, localizacao.Object);
        var request = CriarRequestValido(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

        var result = sut.Create(request);

        Assert.NotNull(result);
        Assert.Single(context.UnidadeAcademias);
    }
}