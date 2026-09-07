using CP1_Academia.Domain.Common;
using CP1_Academia.Domain.Exceptions;

namespace CP1_Academia.Domain.Entities;

public class RedeAcademia : BaseEntity
{
    public string Nome { get; private set; }
    
    public int QntdUnidades { get; private set; }
    
    public string Cnpj { get; private set; }
    
    public DateTime DataFundacao { get; private set; }
    
    public List<UnidadeAcademia> UnidadesAcademia { get; private set; }

    public RedeAcademia(string nome, int qntdUnidades, string cnpj, DateTime dataFundacao)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new DomainException("O nome da rede de academia é obrigatório.");

        if (string.IsNullOrWhiteSpace(cnpj))
            throw new DomainException("O CNPJ é obrigatório.");

        if (qntdUnidades < 0)
            throw new DomainException("A quantidade de unidades não pode ser negativa.");

        Nome = nome;
        QntdUnidades = qntdUnidades;
        Cnpj = cnpj;
        DataFundacao = dataFundacao;
    }
}