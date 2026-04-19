using CP1_Academia.API.Application.DTOs;

namespace CP1_Academia.API.Application.Services;

public interface ILocalizacaoRespository
{
    IReadOnlyList<LocalizacaoResponse> GetAll();
    
    LocalizacaoResponse? GetById(Guid id);
    
    LocalizacaoResponse Create(LocalizacaoRequest request);
    
    bool ExistsById(Guid id);
    
    bool Delete(Guid id);
}