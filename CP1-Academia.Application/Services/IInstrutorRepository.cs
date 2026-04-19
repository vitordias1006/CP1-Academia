using CP1_Academia.API.Application.DTOs;

namespace CP1_Academia.API.Application.Services;

public interface IInstrutorRepository
{
    IReadOnlyList<InstrutorResponse> GetAll();
    
    InstrutorResponse? GetById(Guid id);
    
    InstrutorResponse Create(InstrutorRequest request);
    
    bool ExistsById(Guid id);
    
    bool Delete(Guid id);
}