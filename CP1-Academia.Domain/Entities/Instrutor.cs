namespace ClassLibrary1.Entities;

public class Instrutor : Funcionario
{
    public string Cref { get; private set; }

    public Instrutor(string nome, string cpf, string email, string cargo, double salario, DateTime dataDeContratacao, bool ativo, string cref) : base(nome, cpf, email, cargo, salario, dataDeContratacao, ativo)
    {
        Cref = cref;
    }
}