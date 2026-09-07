using CP1_Academia.API.Application.DTOs;
using CP1_Academia.API.Application.Services;
using CP1_Academia.Domain.Entities;
using CP1_Academia.Domain.Exceptions;
using CP1_Academia.Infrastructure.Persistence;

namespace CP1_Academia.Infrastructure;

public sealed class FuncionarioRepository(
    AcademiaContext context,
    IRepository<Gerente> gerenteRepository,
    IRepository<UnidadeAcademia> unidadeAcademiaRepository) : IFuncionarioRepository
{
    public IReadOnlyList<FuncionarioResponse> GetAll()
    {
        return context.Funcionarios.OrderBy(a => a.Nome)
            .Select(FuncionarioResponse.FromDomain)
            .ToList();
    }

    public FuncionarioResponse? GetById(Guid id)
    {
        var funcionario = context.Funcionarios.FirstOrDefault(m => m.Id == id);
        return funcionario is null ? null : FuncionarioResponse.FromDomain(funcionario);
    }

    public FuncionarioResponse Create(FuncionarioRequest request)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        if (!gerenteRepository.ExistsById(request.GerenteId))
            throw new ResourceNotFoundException(nameof(Gerente), request.GerenteId);

        if (!unidadeAcademiaRepository.ExistsById(request.UnidadeAcademiaId))
            throw new ResourceNotFoundException(nameof(UnidadeAcademia), request.UnidadeAcademiaId);

        var funcionario = request.ToDomain();

        context.Funcionarios.Add(funcionario);
        context.SaveChanges();

        return FuncionarioResponse.FromDomain(funcionario);
    }

    public bool ExistsById(Guid id) => context.Funcionarios.Count(a => a.Id == id) > 0;

    public bool Delete(Guid id)
    {
        var funcionario = context.Funcionarios.FirstOrDefault(a => a.Id == id);
        if (funcionario is null)
            return false;

        context.Funcionarios.Remove(funcionario);
        context.SaveChanges();
        return true;
    }
}