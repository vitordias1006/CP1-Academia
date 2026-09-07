using CP1_Academia.Domain.Common;
using CP1_Academia.Domain.Exceptions;

namespace CP1_Academia.Domain.Entities;

public class AulaExtra : BaseEntity
{
    public string TipoDeAula { get; private set; }
    
    public DateTime HorarioAula { get; private set; }
    
    public int Capacidade { get; private set; }
    
    public Guid FichaTreinoId { get; private set; }
    public List<FichaTreino> FichaTreinos { get; private set; }
    
    public AulaExtra(string tipoDeAula, DateTime horarioAula, int capacidade, Guid fichaTreinoId)
    {
        if (string.IsNullOrWhiteSpace(tipoDeAula))
            throw new DomainException("O tipo de aula é obrigatório.");

        if (capacidade <= 0)
            throw new DomainException("A capacidade da aula deve ser maior que zero.");

        TipoDeAula = tipoDeAula;
        HorarioAula = horarioAula;
        Capacidade = capacidade;
        FichaTreinoId = fichaTreinoId;
    }
}
