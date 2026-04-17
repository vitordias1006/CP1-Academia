using CP1_Academia.API.Domain.Common;

namespace CP1_Academia.API.Domain.Entities;

public class Funcionario : BaseEntity
{
    public string Nome { get; private set; }
    
    public string Cpf { get; private set; }
    
    public string Email { get; private set; }
    
    public string Cargo { get; private set; }
    
    public Guid GerenteId { get; private set; }
    public Gerente Gerente { get; private set; }
    
    public double Salario { get; private set; }
    
    public DateTime DataDeContratacao { get; private set; }
    
    public bool Ativo { get; private set; }
    
    public Guid UnidadeAcademiaId { get; private set; }
    public UnidadeAcademia UnidadeAcademia { get; private set; }

    public Funcionario(string nome, string cpf, string email, string cargo, Guid gerenteId, double salario, DateTime dataDeContratacao, bool ativo, Guid unidadeAcademiaId)
    {
        Nome = nome;
        Cpf = cpf;
        Email = email;
        Cargo = cargo;
        GerenteId = gerenteId;
        Salario = salario;
        DataDeContratacao = dataDeContratacao;
        Ativo = ativo;
        UnidadeAcademiaId = unidadeAcademiaId;
    }
}