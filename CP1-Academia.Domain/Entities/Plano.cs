using ClassLibrary1.Common;

namespace ClassLibrary1.Entities;

public class Plano : BaseEntity
{
    public double Preco { get; private set; }
    
    public DateTime DataDeAssinatura { get; private set; }
    
    public DateTime DataDeRenovacao { get; private set; }
    
    public string TipoPlano { get; private set; }
    
    public bool Fidelidade { get; private set; }
    
    public bool Ativo { get; private set; }
    
    public List<Aluno> Alunos { get; private set; }

    public Plano(double preco, DateTime dataDeAssinatura, DateTime dataDeRenovacao, string tipoPlano, bool fidelidade, bool ativo, List<Aluno> alunos)
    {
        Preco = preco;
        DataDeAssinatura = dataDeAssinatura;
        DataDeRenovacao = dataDeRenovacao;
        TipoPlano = tipoPlano;
        Fidelidade = fidelidade;
        Ativo = ativo;
        Alunos = alunos;
    }
}