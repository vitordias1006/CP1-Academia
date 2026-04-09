using ClassLibrary1.Common;

namespace ClassLibrary1.Entities;

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

    public Funcionario(string nome, string cpf, string email, string cargo, Guid gerenteId, Gerente gerente, double salario, DateTime dataDeContratacao, bool ativo, Guid unidadeAcademiaId, UnidadeAcademia unidadeAcademia)
    {
        Nome = nome;
        Cpf = cpf;
        Email = email;
        Cargo = cargo;
        GerenteId = gerenteId;
        Gerente = gerente;
        Salario = salario;
        DataDeContratacao = dataDeContratacao;
        Ativo = ativo;
        UnidadeAcademiaId = unidadeAcademiaId;
        UnidadeAcademia = unidadeAcademia;
    }
}