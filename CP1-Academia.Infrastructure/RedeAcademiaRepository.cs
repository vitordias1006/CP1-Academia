using CP1_Academia.API.Application.DTOs;
using CP1_Academia.API.Application.Services;
using CP1_Academia.Domain.Exceptions;
using CP1_Academia.Infrastructure.Persistence;

namespace CP1_Academia.Infrastructure;

public sealed class RedeAcademiaRepository (AcademiaContext context) : IRedeAcademiaRepository
{
    public IReadOnlyList<RedeAcademiaResponse> GetAll()
    {
        return context.RedeAcademias.OrderBy(a => a.Nome)
            .Select(RedeAcademiaResponse.FromDomain)
            .ToList();
    }

    public RedeAcademiaResponse? GetById(Guid id)
    {
        var redeAcademia = context.RedeAcademias.FirstOrDefault(m => m.Id == id);
        return redeAcademia is null ? null : RedeAcademiaResponse.FromDomain(redeAcademia);
    }

    public RedeAcademiaResponse Create(RedeAcademiaRequest request)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        if (string.IsNullOrWhiteSpace(request.Nome))
            throw new DomainException("O nome da Rede de Academia é obrigatório");

        var redeAcademia = request.ToDomain();

        context.RedeAcademias.Add(redeAcademia);
        context.SaveChanges();

        return RedeAcademiaResponse.FromDomain(redeAcademia);
    }

    public bool ExistsById(Guid id)
    {
        return context.RedeAcademias.Any(a => a.Id == id);
    }

    public bool Delete(Guid id)
    {
        var redeAcademia = context.RedeAcademias.FirstOrDefault(a => a.Id == id);
        if (redeAcademia is null)
            return false;

        context.RedeAcademias.Remove(redeAcademia);
        context.SaveChanges();

        return true;
    }
}