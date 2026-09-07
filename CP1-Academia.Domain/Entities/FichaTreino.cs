using CP1_Academia.Domain.Common;
using CP1_Academia.Domain.Exceptions;

namespace CP1_Academia.Domain.Entities;

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
        if (string.IsNullOrWhiteSpace(exercicios))
            throw new DomainException("O nome do exercício é obrigatório.");

        if (repeticoes <= 0)
            throw new DomainException("O número de repetições deve ser maior que zero.");

        if (series <= 0)
            throw new DomainException("O número de séries deve ser maior que zero.");

        Exercicios = exercicios;
        Repeticoes = repeticoes;
        Series = series;
        TipoExercicio = tipoExercicio;
        MusculoAlvo = musculoAlvo;
        Observacao = observacao;
        AlunoId = alunoId;
    }
}
