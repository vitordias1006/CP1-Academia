using CP1_Academia.API.Application.DTOs;

namespace CP1_Academia.API.Application.Services;

public interface IRedeAcademiaRepository
{
    IReadOnlyList<RedeAcademiaResponse> GetAll();
    
    RedeAcademiaResponse? GetById(Guid id);
    
    RedeAcademiaResponse Create(RedeAcademiaRequest request);
    
    bool ExistsById(Guid id);
    
    bool Delete(Guid id);
}