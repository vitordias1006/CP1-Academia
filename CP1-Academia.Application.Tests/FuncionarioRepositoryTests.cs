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

public class FuncionarioRepositoryTests
{
    private static AcademiaContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<AcademiaContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AcademiaContext(options);
    }

    private static FuncionarioRequest CriarRequestValido(Guid gerenteId, Guid unidadeId) =>
        new("João", "12345678900", "joao@email.com", "Recepção", gerenteId, 2500, DateTime.Now, true, unidadeId);

    [Fact]
    public void Create_GerenteInexistente_LancaResourceNotFoundException_NaoPersiste()
    {
        var context = CreateInMemoryContext();

        var gerenteRepositoryMock = new Mock<IRepository<Gerente>>();
        gerenteRepositoryMock.Setup(r => r.ExistsById(It.IsAny<Guid>())).Returns(false);

        var unidadeRepositoryMock = new Mock<IRepository<UnidadeAcademia>>();
        unidadeRepositoryMock.Setup(r => r.ExistsById(It.IsAny<Guid>())).Returns(true);

        var sut = new FuncionarioRepository(context, gerenteRepositoryMock.Object, unidadeRepositoryMock.Object);
        var request = CriarRequestValido(Guid.NewGuid(), Guid.NewGuid());

        void Act() => sut.Create(request);

        Assert.Throws<ResourceNotFoundException>(Act);
        Assert.Empty(context.Funcionarios);
        gerenteRepositoryMock.Verify(r => r.ExistsById(It.IsAny<Guid>()), Times.Once);
    }

    [Fact]
    public void Create_UnidadeInexistente_LancaResourceNotFoundException_NaoPersiste()
    {
        var context = CreateInMemoryContext();

        var gerenteRepositoryMock = new Mock<IRepository<Gerente>>();
        gerenteRepositoryMock.Setup(r => r.ExistsById(It.IsAny<Guid>())).Returns(true);

        var unidadeRepositoryMock = new Mock<IRepository<UnidadeAcademia>>();
        unidadeRepositoryMock.Setup(r => r.ExistsById(It.IsAny<Guid>())).Returns(false);

        var sut = new FuncionarioRepository(context, gerenteRepositoryMock.Object, unidadeRepositoryMock.Object);
        var request = CriarRequestValido(Guid.NewGuid(), Guid.NewGuid());

        void Act() => sut.Create(request);

        Assert.Throws<ResourceNotFoundException>(Act);
        Assert.Empty(context.Funcionarios);
    }

    [Fact]
    public void Create_DependenciasExistentes_PersisteFuncionarioUmaVez()
    {
        var context = CreateInMemoryContext();

        var gerenteRepositoryMock = new Mock<IRepository<Gerente>>();
        gerenteRepositoryMock.Setup(r => r.ExistsById(It.IsAny<Guid>())).Returns(true);

        var unidadeRepositoryMock = new Mock<IRepository<UnidadeAcademia>>();
        unidadeRepositoryMock.Setup(r => r.ExistsById(It.IsAny<Guid>())).Returns(true);

        var sut = new FuncionarioRepository(context, gerenteRepositoryMock.Object, unidadeRepositoryMock.Object);
        var request = CriarRequestValido(Guid.NewGuid(), Guid.NewGuid());

        var result = sut.Create(request);

        Assert.NotNull(result);
        Assert.Single(context.Funcionarios);
    }
}