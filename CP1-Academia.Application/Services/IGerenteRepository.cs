using CP1_Academia.API.Application.DTOs;

namespace CP1_Academia.API.Application.Services;

public interface IGerenteRepository
{
    IReadOnlyList<GerenteRequest> GetAll();
    
    GerenteRequest? GetById(Guid id);
    
    GerenteRequest Create(GerenteRequest request);
    
    bool ExistsById(Guid id);
    
    bool Delete(Guid id);
}