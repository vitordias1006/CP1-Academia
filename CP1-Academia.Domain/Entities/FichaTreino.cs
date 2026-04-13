using ClassLibrary1.Common;

namespace ClassLibrary1.Entities;

public class FichaTreino : BaseEntity
{
    public string Exercicios { get; private set; }
    
    public int Repeticoes { get; private set; }
    
    public int Series { get; private set; }
    
    public string TipoExercicio { get; private set; }
    
    public string MusculoAlvo { get; private set; }
    
    public string Observacao { get; private set; }
    
    public List<AulaExtra> AulaExtras { get; private set; }
    
    public Guid AlunoId { get; private set; }
    public Aluno Aluno { get; private set; }
    
    public FichaTreino(string exercicios, int repeticoes, int series, string tipoExercicio, string musculoAlvo, string observacao, Guid alunoId)
    {
        Exercicios = exercicios;
        Repeticoes = repeticoes;
        Series = series;
        TipoExercicio = tipoExercicio;
        MusculoAlvo = musculoAlvo;
        Observacao = observacao;
        AlunoId = alunoId;
    }
}
