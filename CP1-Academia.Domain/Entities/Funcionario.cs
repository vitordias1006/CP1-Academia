using ClassLibrary1.Common;

namespace ClassLibrary1.Entities;

public class Funcionario : BaseEntity
{
    public string Nome { get; private set; }
    
    public string Cpf { get; private set; }
    
    public string Email { get; private set; }
    
    public string Cargo { get; private set; }
    
    public double Salario { get; private set; }
    
    public DateTime DataDeContratacao { get; private set; }
    
    public bool Ativo { get; private set; }

    public Funcionario(string nome, string cpf, string email, string cargo, double salario, DateTime dataDeContratacao, bool ativo)
    {
        Nome = nome;
        Cpf = cpf;
        Email = email;
        Cargo = cargo;
        Salario = salario;
        DataDeContratacao = dataDeContratacao;
        Ativo = ativo;
    }
}