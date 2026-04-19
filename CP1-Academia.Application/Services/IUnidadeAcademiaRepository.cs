using CP1_Academia.API.Application.DTOs;

namespace CP1_Academia.API.Application.Services;

public interface IUnidadeAcademiaRepository
{
    IReadOnlyList<UnidadeAcademiaRequest> GetAll();
    
    UnidadeAcademiaRequest? GetById(Guid id);
    
    UnidadeAcademiaRequest Create(UnidadeAcademiaRequest request);
    
    bool ExistsById(Guid id);
    
    bool Delete(Guid id);
}