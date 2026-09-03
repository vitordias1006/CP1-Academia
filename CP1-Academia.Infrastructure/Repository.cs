using CP1_Academia.API.Application.Services;
using CP1_Academia.Domain.Common;
using CP1_Academia.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CP1_Academia.Infrastructure;

public sealed class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly AcademiaContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(AcademiaContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public IReadOnlyList<T> GetAll()
        => _dbSet.AsNoTracking().ToList();

    public T? GetById(Guid id)
        => _dbSet.FirstOrDefault(e => e.Id == id);

    public void Add(T entity)
    {
        _dbSet.Add(entity);
        _context.SaveChanges();
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
        _context.SaveChanges();
    }

    public bool Delete(Guid id)
    {
        var entity = GetById(id);
        if (entity is null)
            return false;

        _dbSet.Remove(entity);
        _context.SaveChanges();
        return true;
    }

    public bool ExistsById(Guid id)
        => _dbSet.Any(e => e.Id == id);
}