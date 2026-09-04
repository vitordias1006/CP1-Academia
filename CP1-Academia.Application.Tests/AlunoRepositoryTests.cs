using CP1_Academia.API.Application.DTOs;
using CP1_Academia.API.Application.Services;
using CP1_Academia.Domain.Entities;
using CP1_Academia.Domain.Exceptions;
using CP1_Academia.Infrastructure;
using CP1_Academia.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CP1_Academia.Application.Tests;

public class AlunoRepositoryTests
{
    private static AcademiaContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<AcademiaContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AcademiaContext(options);
    }

    private static AlunoRequest CriarRequestValido(Guid planoId) => new()
    {
        Nome = "Carlos",
        Cpf = "11122233344",
        Email = "carlos@email.com",
        Telefone = "11988886666",
        DataMatricula = DateTime.Now,
        Ativo = true,
        PlanoId = planoId
    };

    [Fact]
    public void Create_PlanoInexistente_LancaResourceNotFoundException_NaoPersiste()
    {
        var context = CreateInMemoryContext();

        var planoRepositoryMock = new Mock<IRepository<Plano>>();
        planoRepositoryMock
            .Setup(r => r.ExistsById(It.IsAny<Guid>()))
            .Returns(false);

        var sut = new AlunoRepository(context, planoRepositoryMock.Object);
        var request = CriarRequestValido(Guid.NewGuid());

        void Act() => sut.Create(request);

        Assert.Throws<ResourceNotFoundException>(Act);
        Assert.Empty(context.Alunos);
        planoRepositoryMock.Verify(r => r.ExistsById(It.IsAny<Guid>()), Times.Once);
    }

    [Fact]
    public void Create_PlanoExistente_PersisteAlunoUmaVez()
    { 
        var context = CreateInMemoryContext();

        var planoRepositoryMock = new Mock<IRepository<Plano>>();
        planoRepositoryMock
            .Setup(r => r.ExistsById(It.IsAny<Guid>()))
            .Returns(true);

        var sut = new AlunoRepository(context, planoRepositoryMock.Object);
        var request = CriarRequestValido(Guid.NewGuid());

        var result = sut.Create(request);

        Assert.NotNull(result);
        Assert.Single(context.Alunos);
    }
}