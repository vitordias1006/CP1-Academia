using CP1_Academia.API.Application.DTOs;
using CP1_Academia.API.Application.Services;
using CP1_Academia.Domain.Entities;
using CP1_Academia.Domain.Exceptions;
using CP1_Academia.Infrastructure.Persistence;

namespace CP1_Academia.Infrastructure;

public sealed class UnidadeAcademiaRepository(
    AcademiaContext context,
    IRepository<RedeAcademia> redeAcademiaRepository,
    IRepository<Gerente> gerenteRepository,
    IRepository<Localizacao> localizacaoRepository) : IUnidadeAcademiaRepository
{
    public IReadOnlyList<UnidadeAcademiaResponse> GetAll()
    {
        return context.UnidadeAcademias.OrderBy(a => a.Telefone)
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

        if (!redeAcademiaRepository.ExistsById(request.RedeAcademiaId))
            throw new ResourceNotFoundException(nameof(RedeAcademia), request.RedeAcademiaId);

        if (!gerenteRepository.ExistsById(request.GerenteId))
            throw new ResourceNotFoundException(nameof(Gerente), request.GerenteId);

        if (!localizacaoRepository.ExistsById(request.LocalizacaoId))
            throw new ResourceNotFoundException(nameof(Localizacao), request.LocalizacaoId);

        var unidadeAcademia = request.ToDomain();

        context.UnidadeAcademias.Add(unidadeAcademia);
        context.SaveChanges();

        return UnidadeAcademiaResponse.FromDomain(unidadeAcademia);
    }

    public bool ExistsById(Guid id) => context.UnidadeAcademias.Count(a => a.Id == id) > 0;

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