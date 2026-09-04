using CP1_Academia.API.Application.DTOs;
using CP1_Academia.API.Application.Services;
using CP1_Academia.Domain.Exceptions;
using CP1_Academia.Infrastructure.Persistence;

namespace CP1_Academia.Infrastructure;

public sealed class InstrutorRepository (AcademiaContext context) : IInstrutorRepository
{
    public IReadOnlyList<InstrutorResponse> GetAll()
    {
        return context.Instrutors.OrderBy(a => a.Nome)
            .Select(InstrutorResponse.FromDomain)
            .ToList();
    }

    public InstrutorResponse? GetById(Guid id)
    {
        var instrutor = context.Instrutors.FirstOrDefault(m => m.Id == id);
        return instrutor is null ? null : InstrutorResponse.FromDomain(instrutor);
    }

    public InstrutorResponse Create(InstrutorRequest request)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        if (string.IsNullOrWhiteSpace(request.Nome))
            throw new DomainException("O nome do Instrutor é obrigatório");

        var instrutor = request.ToDomain();

        context.Instrutors.Add(instrutor);
        context.SaveChanges();

        return InstrutorResponse.FromDomain(instrutor);
    }

    public bool ExistsById(Guid id)
    {
        return context.Instrutors.Any(a => a.Id == id);
    }

    public bool Delete(Guid id)
    {
        var instrutor = context.Instrutors.FirstOrDefault(a => a.Id == id);
        if (instrutor is null)
            return false;

        context.Instrutors.Remove(instrutor);
        context.SaveChanges();

        return true;
    }
}