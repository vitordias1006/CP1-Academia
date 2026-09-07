using CP1_Academia.API.Application.DTOs;
using CP1_Academia.API.Application.Services;
using CP1_Academia.Domain.Entities;
using CP1_Academia.Domain.Exceptions;
using CP1_Academia.Infrastructure.Persistence;

namespace CP1_Academia.Infrastructure;

public sealed class FichaTreinoRepository(AcademiaContext context, IRepository<Aluno> alunoRepository) : IFichaTreinoRepository
{
    public IReadOnlyList<FichaTreinoResponse> GetAll()
    {
        return context.FichaTreinos.OrderBy(a => a.Exercicios)
            .Select(FichaTreinoResponse.FromDomain)
            .ToList();
    }

    public FichaTreinoResponse? GetById(Guid id)
    {
        var fichaTreino = context.FichaTreinos.FirstOrDefault(m => m.Id == id);
        return fichaTreino is null ? null : FichaTreinoResponse.FromDomain(fichaTreino);
    }

    public FichaTreinoResponse Create(FichaTreinoRequest request)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        if (!alunoRepository.ExistsById(request.AlunoId))
            throw new ResourceNotFoundException(nameof(Aluno), request.AlunoId);

        var fichaTreino = request.ToDomain();

        context.FichaTreinos.Add(fichaTreino);
        context.SaveChanges();

        return FichaTreinoResponse.FromDomain(fichaTreino);
    }

    public bool ExistsById(Guid id) => context.FichaTreinos.Count(a => a.Id == id) > 0;

    public bool Delete(Guid id)
    {
        var fichaTreino = context.FichaTreinos.FirstOrDefault(a => a.Id == id);
        if (fichaTreino is null)
            return false;

        context.FichaTreinos.Remove(fichaTreino);
        context.SaveChanges();
        return true;
    }
}