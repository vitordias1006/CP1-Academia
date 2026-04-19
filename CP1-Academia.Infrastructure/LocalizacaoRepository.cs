using CP1_Academia.API.Application.DTOs;
using CP1_Academia.API.Application.Services;
using CP1_Academia.Infrastructure.Persistence;

namespace CP1_Academia.Infrastructure;

public sealed class LocalizacaoRepository (AcademiaContext context) : ILocalizacaoRespository
{
    public IReadOnlyList<LocalizacaoResponse> GetAll()
    {
        return context.Localizacoes.OrderBy(a => a.Estado)
            .Select(LocalizacaoResponse.FromDomain)
            .ToList();
    }

    public LocalizacaoResponse? GetById(Guid id)
    {
        var localizacao = context.Localizacoes.FirstOrDefault(m=> m.Id == id);
        
        return localizacao is null ? null : LocalizacaoResponse.FromDomain(localizacao);
    }

    public LocalizacaoResponse Create(LocalizacaoRequest request)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        if (string.IsNullOrWhiteSpace(request.Estado))
            throw new InvalidOperationException("O nome do Estado é obrigatório");
        

        var localizacao = request.ToDomain();

        context.Localizacoes.Add(localizacao);
        context.SaveChanges();

        return LocalizacaoResponse.FromDomain(localizacao);
    }
    
    public bool ExistsById(Guid id)
    {
        return context.Localizacoes.Any(a=> a.Id == id);
    }

    public bool Delete(Guid id)
    {
        var localizacao = context.Localizacoes.FirstOrDefault(a => a.Id == id);
        if (context is null)
            return false;

        context.Localizacoes.Remove(localizacao);
        context.SaveChanges();

        return true;
    }
}