using CP1_Academia.Domain.Common;
using CP1_Academia.Domain.Exceptions;

namespace CP1_Academia.Domain.Entities;

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
        if (string.IsNullOrWhiteSpace(nome))
            throw new DomainException("O nome do aluno é obrigatório.");

        if (string.IsNullOrWhiteSpace(cpf))
            throw new DomainException("O CPF do aluno é obrigatório.");

        if (dataMatricula > DateTime.Now)
            throw new DomainException("A data de matrícula não pode ser no futuro.");

        Nome = nome;
        Cpf = cpf;
        Email = email;
        Telefone = telefone;
        DataMatricula = dataMatricula;
        Ativo = ativo;
        PlanoId = planoId;
    }
}