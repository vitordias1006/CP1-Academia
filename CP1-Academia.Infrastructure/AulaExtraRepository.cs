using CP1_Academia.API.Application.DTOs;
using CP1_Academia.API.Application.Services;
using CP1_Academia.Domain.Exceptions;
using CP1_Academia.Infrastructure.Persistence;

namespace CP1_Academia.Infrastructure;

public sealed class AulaExtraRepository (AcademiaContext context) : IAulaExtraRepository
{
    public IReadOnlyList<AulaExtraResponse> GetAll()
    {
        return context.AulaExtras.OrderBy(a => a.Capacidade)
            .Select(AulaExtraResponse.FromDomain)
            .ToList();
    }

    public AulaExtraResponse? GetById(Guid id)
    {
        var aulaExtra = context.AulaExtras.FirstOrDefault(m => m.Id == id);
        return aulaExtra is null ? null : AulaExtraResponse.FromDomain(aulaExtra);
    }

    public AulaExtraResponse Create(AulaExtraRequest request)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        if (string.IsNullOrWhiteSpace(request.TipoDeAula))
            throw new DomainException("O Tipo de aula é obrigatório");

        var aulaExtra = request.ToDomain();

        context.AulaExtras.Add(aulaExtra);
        context.SaveChanges();

        return AulaExtraResponse.FromDomain(aulaExtra);
    }

    public bool ExistsById(Guid id)
    {
        return context.AulaExtras.Any(a => a.Id == id);
    }

    public bool Delete(Guid id)
    {
        var aulaExtra = context.AulaExtras.FirstOrDefault(a => a.Id == id);
        if (aulaExtra is null)
            return false;

        context.AulaExtras.Remove(aulaExtra);
        context.SaveChanges();

        return true;
    }
}