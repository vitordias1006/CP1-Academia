using CP1_Academia.API.Application.DTOs;

namespace CP1_Academia.API.Application.Services;

public interface IFichaTreinoRepository
{
    IReadOnlyList<FichaTreinoRequest> GetAll();
    
    FichaTreinoRequest? GetById(Guid id);
    
    FichaTreinoRequest Create(FichaTreinoRequest request);
    
    bool ExistsById(Guid id);
    
    bool Delete(Guid id);
}