using ClassLibrary1.Common;

namespace ClassLibrary1.Entities;

public class RedeAcademia : BaseEntity
{
    public string Nome { get; private set; }
    
    public int QntdUnidades { get; private set; }
    
    public string Cnpj { get; private set; }
    
    public DateTime DataFundacao { get; private set; }
    
    public List<UnidadeAcademia> UnidadesAcademia { get; private set; }

    public RedeAcademia(string nome, int qntdUnidades, string cnpj, DateTime dataFundacao)
    {
        Nome = nome;
        QntdUnidades = qntdUnidades;
        Cnpj = cnpj;
        DataFundacao = dataFundacao;
    }
}