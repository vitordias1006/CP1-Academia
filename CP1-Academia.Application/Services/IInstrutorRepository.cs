using CP1_Academia.API.Application.DTOs;

namespace CP1_Academia.API.Application.Services;

public interface IInstrutorRepository
{
    IReadOnlyList<InstrutorRequest> GetAll();
    
    InstrutorRequest? GetById(Guid id);
    
    InstrutorRequest Create(InstrutorRequest request);
    
    bool ExistsById(Guid id);
    
    bool Delete(Guid id);
}