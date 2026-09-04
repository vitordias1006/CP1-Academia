using CP1_Academia.API.Application.DTOs;
using CP1_Academia.API.Application.Services;
using CP1_Academia.Domain.Exceptions;
using CP1_Academia.Infrastructure.Persistence;

namespace CP1_Academia.Infrastructure;

public sealed class GerenteRepository (AcademiaContext context) : IGerenteRepository
{
    public IReadOnlyList<GerenteResponse> GetAll()
    {
        return context.Gerentes.OrderBy(a => a.Nome)
            .Select(GerenteResponse.FromDomain)
            .ToList();
    }

    public GerenteResponse? GetById(Guid id)
    {
        var gerente = context.Gerentes.FirstOrDefault(m => m.Id == id);
        return gerente is null ? null : GerenteResponse.FromDomain(gerente);
    }

    public GerenteResponse Create(GerenteRequest request)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        if (string.IsNullOrWhiteSpace(request.Nome))
            throw new DomainException("O nome do Gerente é obrigatório");

        var gerente = request.ToDomain();

        context.Gerentes.Add(gerente);
        context.SaveChanges();

        return GerenteResponse.FromDomain(gerente);
    }

    public bool ExistsById(Guid id)
    {
        return context.Gerentes.Any(a => a.Id == id);
    }

    public bool Delete(Guid id)
    {
        var gerente = context.Gerentes.FirstOrDefault(a => a.Id == id);
        if (gerente is null)
            return false;

        context.Gerentes.Remove(gerente);
        context.SaveChanges();

        return true;
    }
}