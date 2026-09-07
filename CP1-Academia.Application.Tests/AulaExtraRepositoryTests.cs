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

public class AulaExtraRepositoryTests
{
    private static AcademiaContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<AcademiaContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AcademiaContext(options);
    }

    private static AulaExtraRequest CriarRequestValido(Guid fichaTreinoId) =>
        new("Spinning", DateTime.Now.AddHours(1), 15, fichaTreinoId);

    [Fact]
    public void Create_FichaTreinoInexistente_LancaResourceNotFoundException_NaoPersiste()
    {
        var context = CreateInMemoryContext();
        var fichaTreinoRepositoryMock = new Mock<IRepository<FichaTreino>>();
        fichaTreinoRepositoryMock.Setup(r => r.ExistsById(It.IsAny<Guid>())).Returns(false);

        var sut = new AulaExtraRepository(context, fichaTreinoRepositoryMock.Object);
        var request = CriarRequestValido(Guid.NewGuid());

        void Act() => sut.Create(request);

        Assert.Throws<ResourceNotFoundException>(Act);
        Assert.Empty(context.AulaExtras);
        fichaTreinoRepositoryMock.Verify(r => r.ExistsById(It.IsAny<Guid>()), Times.Once);
    }

    [Fact]
    public void Create_FichaTreinoExistente_PersisteAulaExtraUmaVez()
    {
        var context = CreateInMemoryContext();
        var fichaTreinoRepositoryMock = new Mock<IRepository<FichaTreino>>();
        fichaTreinoRepositoryMock.Setup(r => r.ExistsById(It.IsAny<Guid>())).Returns(true);

        var sut = new AulaExtraRepository(context, fichaTreinoRepositoryMock.Object);
        var request = CriarRequestValido(Guid.NewGuid());

        var result = sut.Create(request);

        Assert.NotNull(result);
        Assert.Single(context.AulaExtras);
    }
}