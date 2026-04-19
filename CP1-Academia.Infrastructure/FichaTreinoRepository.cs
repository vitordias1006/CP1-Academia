using CP1_Academia.API.Application.DTOs;
using CP1_Academia.API.Application.Services;
using CP1_Academia.Infrastructure.Persistence;

namespace CP1_Academia.Infrastructure;

public sealed class FichaTreinoRepository (AcademiaContext context) : IFichaTreinoRepository
{
    public IReadOnlyList<FichaTreinoRequest> GetAll()
    {
        return context.FichaTreinos.OrderBy(a => a.Aluno)
            .Select(FichaTreinoRequest.FromDomain)
            .ToList();
    }

    public FichaTreinoRequest? GetById(Guid id)
    {
        var fichaTreino = context.FichaTreinos.FirstOrDefault(m=> m.Id == id);
        
        return fichaTreino is null ? null : FichaTreinoRequest.FromDomain(fichaTreino);
    }

    public FichaTreinoRequest Create(FichaTreinoRequest request)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        if (string.IsNullOrWhiteSpace(request.Exercicios))
            throw new InvalidOperationException("O nome do exercicio é obrigatório");
        

        var fichaTreino = request.ToDomain();

        context.FichaTreinos.Add(fichaTreino);
        context.SaveChanges();

        return FichaTreinoRequest.FromDomain(fichaTreino);
    }
    
    public bool ExistsById(Guid id)
    {
        return context.FichaTreinos.Any(a=> a.Id == id);
    }

    public bool Delete(Guid id)
    {
        var fichaTreino = context.FichaTreinos.FirstOrDefault(a => a.Id == id);
        if (context is null)
            return false;

        context.FichaTreinos.Remove(fichaTreino);
        context.SaveChanges();

        return true;
    }
}