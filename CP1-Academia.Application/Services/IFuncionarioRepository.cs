using CP1_Academia.API.Application.DTOs;

namespace CP1_Academia.API.Application.Services;

public interface IFuncionarioRepository
{
    IReadOnlyList<FuncionarioResponse> GetAll();
    
    FuncionarioResponse? GetById(Guid id);
    
    FuncionarioResponse Create(FuncionarioRequest request);
    
    bool ExistsById(Guid id);
    
    bool Delete(Guid id);
}