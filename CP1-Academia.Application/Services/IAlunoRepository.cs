using CP1_Academia.API.Application.DTOs;

namespace CP1_Academia.API.Application.Services;

public interface IAlunoRepository
{
    IReadOnlyList<AlunoResponse> GetAll();
    
    AlunoResponse? GetById(Guid id);
    
    AlunoResponse Create(AlunoRequest request);
    
    bool ExistsByTitle(string title);
    
    bool ExistsById(Guid id);
    
    bool Delete(Guid id);
}