using CP1_Academia.API.Application.DTOs;

namespace CP1_Academia.API.Application.Services;

public interface ILocalizacaoRespository
{
    IReadOnlyList<LocalizacaoRequest> GetAll();
    
    LocalizacaoRequest? GetById(Guid id);
    
    LocalizacaoRequest Create(LocalizacaoRequest request);
    
    bool ExistsById(Guid id);
    
    bool Delete(Guid id);
}