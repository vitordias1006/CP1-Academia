using CP1_Academia.API.Application.DTOs;

namespace CP1_Academia.API.Application.Services;

public interface IUnidadeAcademiaRepository
{
    IReadOnlyList<UnidadeAcademiaResponse> GetAll();
    
    UnidadeAcademiaResponse? GetById(Guid id);
    
    UnidadeAcademiaResponse Create(UnidadeAcademiaRequest request);
    
    bool ExistsById(Guid id);
    
    bool Delete(Guid id);
}