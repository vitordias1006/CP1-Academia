using CP1_Academia.API.Application.DTOs;
using CP1_Academia.API.Application.Services;
using CP1_Academia.Infrastructure.Persistence;

namespace CP1_Academia.Infrastructure;

public class PlanoRepository (AcademiaContext context) : IPlanoRepository
{
    public IReadOnlyList<PlanoResponse> GetAll()
    {
        return context.Planos.OrderBy(a => a.Preco)
            .Select(PlanoResponse.FromDomain)
            .ToList();
    }

    public PlanoResponse? GetById(Guid id)
    {
        var plano = context.Planos.FirstOrDefault(m => m.Id == id);
        return plano is null ? null : PlanoResponse.FromDomain(plano);
    }

    public PlanoResponse Create(PlanoRequest request)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var plano = request.ToDomain();

        context.Planos.Add(plano);
        context.SaveChanges();

        return PlanoResponse.FromDomain(plano);
    }

    public bool ExistsById(Guid id)
    {
        return context.Planos.Any(a => a.Id == id);
    }

    public bool Delete(Guid id)
    {
        var plano = context.Planos.FirstOrDefault(a => a.Id == id);
        if (plano is null)
            return false;

        context.Planos.Remove(plano);
        context.SaveChanges();

        return true;
    }
}