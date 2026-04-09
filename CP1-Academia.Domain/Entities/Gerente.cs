namespace ClassLibrary1.Entities;

public class Gerente : Funcionario
{
   public double Comissao { get; private set; }
   
   public DateTime PeriodoDeLideranca { get; private set; }
   
   public String AreaDeResponsabilidade { get; private set; }
   
   public String NivelDeLideranca { get; private set; }
   
   public List<Funcionario> Funcionarios { get; private set; }
   public List<UnidadeAcademia> UnidadesAcademia { get; private set; }

   public Gerente(string nome, string cpf, string email, string cargo, Guid gerenteId, Gerente gerente, double salario, DateTime dataDeContratacao, bool ativo, Guid unidadeAcademiaId, UnidadeAcademia unidadeAcademia, double comissao, DateTime periodoDeLideranca, string areaDeResponsabilidade, string nivelDeLideranca, List<Funcionario> funcionarios, List<UnidadeAcademia> unidadesAcademia) : base(nome, cpf, email, cargo, gerenteId, gerente, salario, dataDeContratacao, ativo, unidadeAcademiaId, unidadeAcademia)
   {
      Comissao = comissao;
      PeriodoDeLideranca = periodoDeLideranca;
      AreaDeResponsabilidade = areaDeResponsabilidade;
      NivelDeLideranca = nivelDeLideranca;
      Funcionarios = funcionarios;
      UnidadesAcademia = unidadesAcademia;
   }
}