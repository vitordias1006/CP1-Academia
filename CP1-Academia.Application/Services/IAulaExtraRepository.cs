using CP1_Academia.API.Application.DTOs;

namespace CP1_Academia.API.Application.Services;

public interface IAulaExtraRepository
{
    IReadOnlyList<AulaExtraResponse> GetAll();
    
    AulaExtraResponse? GetById(Guid id);
    
    AulaExtraResponse Create(AulaExtraRequest request);
    
    bool ExistsById(Guid id);
    
    bool Delete(Guid id);
}