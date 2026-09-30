using CP1_Academia.API.Application.DTOs;
using CP1_Academia.API.Application.Services;
using CP1_Academia.Domain.Entities;
using CP1_Academia.Infrastructure;
using CP1_Academia.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CP1_Academia.Application.Tests;

public class PaginacaoTests
{
    [Theory]
    [InlineData(0, 20)]
    [InlineData(-1, 20)]
    [InlineData(1, 0)]
    [InlineData(1, -5)]
    [InlineData(1, 101)]
    [InlineData(1, 9999)]
    public void PageRequest_ParametrosInvalidos_LancaArgumentException(int page, int pageSize)
    {
        var ex = Assert.Throws<ArgumentException>(() => PageRequest.Create(page, pageSize));

        Assert.False(string.IsNullOrWhiteSpace(ex.Message));
    }

    [Theory]
    [InlineData(1, 1)]
    [InlineData(1, 20)]
    [InlineData(3, 100)]
    [InlineData(1000, 50)]
    public void PageRequest_ParametrosValidos_CriaComOsValoresInformados(int page, int pageSize)
    {
        var request = PageRequest.Create(page, pageSize);

        Assert.Equal(page, request.Page);
        Assert.Equal(pageSize, request.PageSize);
    }

    [Fact]
    public void PageRequest_Padroes_SaoPagina1Tamanho20()
    {
        var request = PageRequest.Create();

        Assert.Equal(1, request.Page);
        Assert.Equal(20, request.PageSize);
    }


    private static AlunoRepository CriarRepositorioComCincoAlunos()
    {
        var options = new DbContextOptionsBuilder<AcademiaContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        var context = new AcademiaContext(options);

        var planoId = Guid.NewGuid();
        foreach (var nome in new[] { "Ana", "Bruno", "Carla", "Diego", "Elisa" })
        {
            context.Alunos.Add(new Aluno(
                nome, $"cpf-{nome}", $"{nome}@email.com", "11999999999",
                DateTime.Now.AddDays(-1), true, planoId));
        }
        context.SaveChanges();

        return new AlunoRepository(context, new Mock<IRepository<Plano>>().Object);
    }

    [Fact]
    public void GetPaged_Pagina1e2_NaoSeSobrepoem_ETotaisFecham()
    {
        var sut = CriarRepositorioComCincoAlunos();

        var p1 = sut.GetPaged(PageRequest.Create(1, 2));
        var p2 = sut.GetPaged(PageRequest.Create(2, 2));

        Assert.Equal(5, p1.TotalItems);
        Assert.Equal(3, p1.TotalPages);             
        Assert.Equal(2, p1.Items.Count);
        Assert.Equal(2, p2.Items.Count);
        Assert.Empty(p1.Items.Select(a => a.Id).Intersect(p2.Items.Select(a => a.Id)));
        Assert.Equal(new[] { "Ana", "Bruno" }, p1.Items.Select(a => a.Nome));
        Assert.Equal(new[] { "Carla", "Diego" }, p2.Items.Select(a => a.Nome));
    }

    [Fact]
    public void GetPaged_PaginaAlemDoTotal_RetornaItemsVazio_SemErro()
    {
        var sut = CriarRepositorioComCincoAlunos();

        var result = sut.GetPaged(PageRequest.Create(999999, 20));

        Assert.Empty(result.Items);
        Assert.Equal(5, result.TotalItems);
    }
}