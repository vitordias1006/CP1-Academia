using CP1_Academia.API.Application.DTOs;

namespace CP1_Academia.API.Application.Services;

public interface IPlanoRepository
{
    IReadOnlyList<PlanoResponse> GetAll();
    
    PlanoResponse? GetById(Guid id);
    
    PlanoResponse Create(PlanoRequest request);
    
    bool ExistsById(Guid id);
    
    bool Delete(Guid id);
}