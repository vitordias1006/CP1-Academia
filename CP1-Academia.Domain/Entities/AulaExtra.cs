using ClassLibrary1.Common;

namespace ClassLibrary1.Entities;

public class AulaExtra : BaseEntity
{
    public string TipoDeAula { get; private set; }
    
    public DateTime HorarioAula { get; private set; }
    
    public int Capacidade { get; private set; }
    
    public Guid FichaTreinoId { get; private set; }
    public FichaTreino FichaTreino { get; private set; }
    
    public AulaExtra(string tipoDeAula, DateTime horarioAula, int capacidade, Guid fichaTreinoId, FichaTreino fichaTreino)
    {
        TipoDeAula = tipoDeAula;
        HorarioAula = horarioAula;
        Capacidade = capacidade;
        FichaTreinoId = fichaTreinoId;
        FichaTreino = fichaTreino;
    }
}
