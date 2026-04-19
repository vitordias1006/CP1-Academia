using CP1_Academia.API.Application.DTOs;
using CP1_Academia.API.Application.Services;
using CP1_Academia.Infrastructure.Persistence;

namespace CP1_Academia.Infrastructure;

public sealed class RedeAcademiaRepository (AcademiaContext context) : IRedeAcademiaRepository
{
    public IReadOnlyList<RedeAcademiaRequest> GetAll()
    {
        return context.RedeAcademias.OrderBy(a => a.Nome)
            .Select(RedeAcademiaRequest.FromDomain)
            .ToList();
    }

    public RedeAcademiaRequest? GetById(Guid id)
    {
        var redeAcademia = context.RedeAcademias.FirstOrDefault(m=> m.Id == id);
        
        return redeAcademia is null ? null : RedeAcademiaRequest.FromDomain(redeAcademia);
    }

    public RedeAcademiaRequest Create(RedeAcademiaRequest request)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        if (string.IsNullOrWhiteSpace(request.Nome))
            throw new InvalidOperationException("O nome da Rede de Academia é obrigatório");
        

        var redeAcademia = request.ToDomain();

        context.RedeAcademias.Add(redeAcademia);
        context.SaveChanges();

        return RedeAcademiaRequest.FromDomain(redeAcademia);
    }
    
    public bool ExistsById(Guid id)
    {
        return context.RedeAcademias.Any(a=> a.Id == id);
    }

    public bool Delete(Guid id)
    {
        var redeAcademia = context.RedeAcademias.FirstOrDefault(a => a.Id == id);
        if (context is null)
            return false;

        context.RedeAcademias.Remove(redeAcademia);
        context.SaveChanges();

        return true;
    }
}