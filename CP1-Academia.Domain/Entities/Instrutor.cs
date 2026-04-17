namespace CP1_Academia.API.Domain.Entities;

public class Instrutor : Funcionario
{
    public string Cref { get; private set; }

    public Instrutor(string nome, string cpf, string email, string cargo, Guid gerenteId, double salario, DateTime dataDeContratacao, bool ativo, Guid unidadeAcademiaId, string cref) : base(nome, cpf, email, cargo, gerenteId, salario, dataDeContratacao, ativo, unidadeAcademiaId)
    {
        Cref = cref;
    }
}