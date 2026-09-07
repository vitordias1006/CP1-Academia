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

public class FichaTreinoRepositoryTests
{
    private static AcademiaContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<AcademiaContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AcademiaContext(options);
    }

    private static FichaTreinoRequest CriarRequestValido(Guid alunoId) =>
        new("Supino reto", 12, 4, "Força", "Peitoral", "Observação", alunoId);

    [Fact]
    public void Create_AlunoInexistente_LancaResourceNotFoundException_NaoPersiste()
    {
        var context = CreateInMemoryContext();
        var alunoRepositoryMock = new Mock<IRepository<Aluno>>();
        alunoRepositoryMock.Setup(r => r.ExistsById(It.IsAny<Guid>())).Returns(false);

        var sut = new FichaTreinoRepository(context, alunoRepositoryMock.Object);
        var request = CriarRequestValido(Guid.NewGuid());

        void Act() => sut.Create(request);

        Assert.Throws<ResourceNotFoundException>(Act);
        Assert.Empty(context.FichaTreinos);
        alunoRepositoryMock.Verify(r => r.ExistsById(It.IsAny<Guid>()), Times.Once);
    }

    [Fact]
    public void Create_AlunoExistente_PersisteFichaUmaVez()
    {
        var context = CreateInMemoryContext();
        var alunoRepositoryMock = new Mock<IRepository<Aluno>>();
        alunoRepositoryMock.Setup(r => r.ExistsById(It.IsAny<Guid>())).Returns(true);

        var sut = new FichaTreinoRepository(context, alunoRepositoryMock.Object);
        var request = CriarRequestValido(Guid.NewGuid());

        var result = sut.Create(request);

        Assert.NotNull(result);
        Assert.Single(context.FichaTreinos);
    }
}