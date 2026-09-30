using CP1_Academia.API.Application.DTOs;
using CP1_Academia.API.Application.Services;
using CP1_Academia.Domain.Entities;
using CP1_Academia.Domain.Exceptions;
using CP1_Academia.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CP1_Academia.Infrastructure;

public sealed class AlunoRepository(AcademiaContext context, IRepository<Plano> planoRepository) : IAlunoRepository
{
    public IReadOnlyList<AlunoResponse> GetAll()
    {
        return context.Alunos.OrderBy(a => a.Nome)
            .Select(AlunoResponse.FromDomain)
            .ToList();
    }
    
    public PagedResult<AlunoResponse> GetPaged(PageRequest pageRequest)
    {
        // IQueryable — nada é executado ainda
        var query = context.Alunos.AsNoTracking();

        // 1) COUNT no banco
        var totalItems = query.Count();

        // Página além do total => 200 com items vazio (não é erro).
        // Também evita estourar o int do Skip quando 'page' é gigante.
        if (pageRequest.Offset >= totalItems)
            return PagedResult<AlunoResponse>.Create(pageRequest, totalItems, []);

        // 2) ORDER BY + SKIP + TAKE no banco (Oracle: OFFSET ... FETCH NEXT)
        var alunos = query
            .OrderBy(a => a.Nome)
            .ThenBy(a => a.Id)                       // desempate => ordem reproduzível
            .Skip((int)pageRequest.Offset)
            .Take(pageRequest.PageSize)
            .ToList();                               // só aqui materializa (no máximo pageSize linhas)

        // 3) Mapeia para DTO já com a página pequena em memória
        var items = alunos.Select(AlunoResponse.FromDomain).ToList();

        return PagedResult<AlunoResponse>.Create(pageRequest, totalItems, items);
    }

    public AlunoResponse? GetById(Guid id)
    {
        var aluno = context.Alunos.FirstOrDefault(m => m.Id == id);
        return aluno is null ? null : AlunoResponse.FromDomain(aluno);
    }

    public AlunoResponse Create(AlunoRequest request)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        if (!planoRepository.ExistsById(request.PlanoId))
            throw new ResourceNotFoundException(nameof(Plano), request.PlanoId);

        var aluno = request.ToDomain();

        context.Alunos.Add(aluno);
        context.SaveChanges();

        return AlunoResponse.FromDomain(aluno);
    }

    public bool ExistsById(Guid id)
    {
        return context.Alunos.Count(a => a.Id == id) > 0;
    }

    public bool Delete(Guid id)
    {
        var aluno = context.Alunos.FirstOrDefault(a => a.Id == id);
        if (aluno is null)
            return false;

        context.Alunos.Remove(aluno);
        context.SaveChanges();

        return true;
    }
}