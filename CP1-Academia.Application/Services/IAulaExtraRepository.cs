using CP1_Academia.API.Application.DTOs;

namespace CP1_Academia.API.Application.Services;

public interface IAulaExtraRepository
{
    IReadOnlyList<AulaExtraRequest> GetAll();
    
    AulaExtraRequest? GetById(Guid id);
    
    AulaExtraRequest Create(AulaExtraRequest request);
    
    bool ExistsById(Guid id);
    
    bool Delete(Guid id);
}