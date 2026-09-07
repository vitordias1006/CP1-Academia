using CP1_Academia.Domain.Common;
using CP1_Academia.Domain.Exceptions;

namespace CP1_Academia.Domain.Entities;

public class Plano : BaseEntity
{
    public double Preco { get; private set; }
    
    public DateTime DataDeAssinatura { get; private set; }
    
    public DateTime DataDeRenovacao { get; private set; }
    
    public string TipoPlano { get; private set; }
    
    public bool Fidelidade { get; private set; }
    
    public bool Ativo { get; private set; }
    
    public List<Aluno> Alunos { get; private set; }

    public Plano(double preco, DateTime dataDeAssinatura, DateTime dataDeRenovacao, string tipoPlano, bool fidelidade, bool ativo)
    {
        if (preco <= 0)
            throw new DomainException("O preço do plano deve ser maior que zero.");

        if (string.IsNullOrWhiteSpace(tipoPlano))
            throw new DomainException("O tipo de plano é obrigatório.");

        if (dataDeRenovacao < dataDeAssinatura)
            throw new DomainException("A data de renovação não pode ser anterior à data de assinatura.");

        Preco = preco;
        DataDeAssinatura = dataDeAssinatura;
        DataDeRenovacao = dataDeRenovacao;
        TipoPlano = tipoPlano;
        Fidelidade = fidelidade;
        Ativo = ativo;
    }
}