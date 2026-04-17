using CP1_Academia.API.Domain.Common;

namespace CP1_Academia.API.Domain.Entities;

public class AulaExtra : BaseEntity
{
    public string TipoDeAula { get; private set; }
    
    public DateTime HorarioAula { get; private set; }
    
    public int Capacidade { get; private set; }
    
    public Guid FichaTreinoId { get; private set; }
    public List<FichaTreino> FichaTreinos { get; private set; }
    
    public AulaExtra(string tipoDeAula, DateTime horarioAula, int capacidade, Guid fichaTreinoId)
    {
        TipoDeAula = tipoDeAula;
        HorarioAula = horarioAula;
        Capacidade = capacidade;
        FichaTreinoId = fichaTreinoId;
    }
}
