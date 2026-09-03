using CP1_Academia.Domain.Common;

namespace CP1_Academia.API.Application.Services;

public interface IRepository<T> where T : BaseEntity
{
    IReadOnlyList<T> GetAll();

    T? GetById(Guid id);

    void Add(T entity);

    void Update(T entity);

    bool Delete(Guid id);

    bool ExistsById(Guid id); 
}