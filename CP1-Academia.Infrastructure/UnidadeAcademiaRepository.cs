using CP1_Academia.API.Application.DTOs;
using CP1_Academia.API.Application.Services;
using CP1_Academia.Domain.Exceptions;
using CP1_Academia.Infrastructure.Persistence;

namespace CP1_Academia.Infrastructure;

public sealed class UnidadeAcademiaRepository (AcademiaContext context) : IUnidadeAcademiaRepository
{
    public IReadOnlyList<UnidadeAcademiaResponse> GetAll()
    {
        return context.UnidadeAcademias.OrderBy(a => a.Gerente)
            .Select(UnidadeAcademiaResponse.FromDomain)
            .ToList();
    }

    public UnidadeAcademiaResponse? GetById(Guid id)
    {
        var unidadeAcademia = context.UnidadeAcademias.FirstOrDefault(m => m.Id == id);
        return unidadeAcademia is null ? null : UnidadeAcademiaResponse.FromDomain(unidadeAcademia);
    }

    public UnidadeAcademiaResponse Create(UnidadeAcademiaRequest request)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        if (string.IsNullOrWhiteSpace(request.Telefone))
            throw new DomainException("O telefone da unidade da academia é obrigatório");

        var unidadeAcademia = request.ToDomain();

        context.UnidadeAcademias.Add(unidadeAcademia);
        context.SaveChanges();

        return UnidadeAcademiaResponse.FromDomain(unidadeAcademia);
    }

    public bool ExistsById(Guid id)
    {
        return context.UnidadeAcademias.Any(a => a.Id == id);
    }

    public bool Delete(Guid id)
    {
        var unidadeAcademia = context.UnidadeAcademias.FirstOrDefault(a => a.Id == id);
        if (unidadeAcademia is null)
            return false;

        context.UnidadeAcademias.Remove(unidadeAcademia);
        context.SaveChanges();

        return true;
    }
}