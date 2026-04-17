using CP1_Academia.API.Domain.Common;

namespace CP1_Academia.API.Domain.Entities;

public class Aluno : BaseEntity
{
    public string Nome { get; private set; }
    
    public string Cpf { get; private set; }
    
    public string Email { get; private set; }
    
    public string Telefone { get; private set; }
    
    public DateTime DataMatricula { get; private set; }
    
    public bool Ativo { get; private set; }
    
    public Guid PlanoId { get; private set; }
    public Plano Plano { get; private set; }
    
    public FichaTreino FichaTreino { get; private set; }

    public Aluno(string nome, string cpf, string email, string telefone, DateTime dataMatricula, bool ativo, Guid planoId)
    {
        Nome = nome;
        Cpf = cpf;
        Email = email;
        Telefone = telefone;
        DataMatricula = dataMatricula;
        Ativo = ativo;
        PlanoId = planoId;
    }
}