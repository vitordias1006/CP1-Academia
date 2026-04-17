using CP1_Academia.API.Application.DTOs;
using CP1_Academia.API.Application.Services;
using CP1_Academia.Infrastructure.Persistence;

namespace CP1_Academia.Infrastructure;

public sealed class AlunoRepository (AcademiaContext context) : IAlunoRepository
{
    public IReadOnlyList<AlunoResponse> GetAll()
    {
        return context.Alunos.OrderBy(a => a.Nome)
            .Select(AlunoResponse.FromDomain)
            .ToList();
    }

    public AlunoResponse? GetById(Guid id)
    {
        var aluno = context.Alunos.FirstOrDefault(m=> m.Id == id);
        
        return aluno is null ? null : AlunoResponse.FromDomain(aluno);
    }

    public AlunoResponse Create(AlunoRequest request)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        if (string.IsNullOrWhiteSpace(request.Nome))
            throw new InvalidOperationException("O título do filme é obrigatório");

        if (ExistsByTitle(request.Nome))
            throw new InvalidOperationException("Já existe um filme com este título");

        var aluno = request.ToDomain();

        context.Alunos.Add(aluno);
        context.SaveChanges();

        return AlunoResponse.FromDomain(aluno);
    }

    public bool ExistsByTitle(string title)
    {
        throw new NotImplementedException();
    }

    public bool ExistsById(Guid id)
    {
        return context.Alunos.Any(a=> a.Id == id);
    }

    public bool Delete(Guid id)
    {
        var aluno = context.Alunos.FirstOrDefault(a => a.Id == id);
        if (context is null)
            return false;

        context.Alunos.Remove(aluno);
        context.SaveChanges();

        return true;
    }
}