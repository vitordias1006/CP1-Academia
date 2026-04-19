using CP1_Academia.API.Application.DTOs;
using CP1_Academia.API.Application.Services;
using CP1_Academia.Infrastructure.Persistence;

namespace CP1_Academia.Infrastructure;

public sealed class GerenteRepository (AcademiaContext context) : IGerenteRepository
{
    public IReadOnlyList<GerenteRequest> GetAll()
    {
        return context.Gerentes.OrderBy(a => a.Nome)
            .Select(GerenteRequest.FromDomain)
            .ToList();
    }

    public GerenteRequest? GetById(Guid id)
    {
        var gerente = context.Gerentes.FirstOrDefault(m=> m.Id == id);
        
        return gerente is null ? null : GerenteRequest.FromDomain(gerente);
    }

    public GerenteRequest Create(GerenteRequest request)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        if (string.IsNullOrWhiteSpace(request.Nome))
            throw new InvalidOperationException("O nome do Gerente é obrigatório");
        

        var gerente = request.ToDomain();

        context.Gerentes.Add(gerente);
        context.SaveChanges();

        return GerenteRequest.FromDomain(gerente);
    }
    
    public bool ExistsById(Guid id)
    {
        return context.Gerentes.Any(a=> a.Id == id);
    }

    public bool Delete(Guid id)
    {
        var gerente = context.Gerentes.FirstOrDefault(a => a.Id == id);
        if (context is null)
            return false;

        context.Gerentes.Remove(gerente);
        context.SaveChanges();

        return true;
    }
}