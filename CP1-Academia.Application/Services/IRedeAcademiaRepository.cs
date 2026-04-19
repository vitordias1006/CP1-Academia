using CP1_Academia.API.Application.DTOs;

namespace CP1_Academia.API.Application.Services;

public interface IRedeAcademiaRepository
{
    IReadOnlyList<RedeAcademiaRequest> GetAll();
    
    RedeAcademiaRequest? GetById(Guid id);
    
    RedeAcademiaRequest Create(RedeAcademiaRequest request);
    
    bool ExistsById(Guid id);
    
    bool Delete(Guid id);
}