namespace ClassLibrary1.Entities;

public class Instrutor : Funcionario
{
    public string Cref { get; private set; }

    public Instrutor(string nome, string cpf, string email, string cargo, Guid gerenteId, Gerente gerente, double salario, DateTime dataDeContratacao, bool ativo, Guid unidadeAcademiaId, UnidadeAcademia unidadeAcademia, string cref) : base(nome, cpf, email, cargo, gerenteId, gerente, salario, dataDeContratacao, ativo, unidadeAcademiaId, unidadeAcademia)
    {
        Cref = cref;
    }
}