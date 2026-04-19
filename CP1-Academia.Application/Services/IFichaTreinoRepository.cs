using CP1_Academia.API.Application.DTOs;

namespace CP1_Academia.API.Application.Services;

public interface IFichaTreinoRepository
{
    IReadOnlyList<FichaTreinoResponse> GetAll();
    
    FichaTreinoResponse? GetById(Guid id);
    
    FichaTreinoResponse Create(FichaTreinoRequest request);
    
    bool ExistsById(Guid id);
    
    bool Delete(Guid id);
}