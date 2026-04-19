using CP1_Academia.API.Application.DTOs;

namespace CP1_Academia.API.Application.Services;

public interface IFuncionarioRepository
{
    IReadOnlyList<FuncionarioRequest> GetAll();
    
    FuncionarioRequest? GetById(Guid id);
    
    FuncionarioRequest Create(FuncionarioRequest request);
    
    bool ExistsById(Guid id);
    
    bool Delete(Guid id);
}