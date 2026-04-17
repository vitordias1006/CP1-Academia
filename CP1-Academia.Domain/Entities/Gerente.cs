namespace CP1_Academia.API.Domain.Entities;

public class Gerente : Funcionario
{
   public double Comissao { get; private set; }
   
   public DateTime PeriodoDeLideranca { get; private set; }
   
   public String AreaDeResponsabilidade { get; private set; }
   
   public String NivelDeLideranca { get; private set; }
   
   public List<Funcionario> Funcionarios { get; private set; }
   public List<UnidadeAcademia> UnidadesAcademia { get; private set; }

   public Gerente(string nome, string cpf, string email, string cargo, Guid gerenteId, double salario, DateTime dataDeContratacao, bool ativo, Guid unidadeAcademiaId, double comissao, DateTime periodoDeLideranca, string areaDeResponsabilidade, string nivelDeLideranca) : base(nome, cpf, email, cargo, gerenteId, salario, dataDeContratacao, ativo, unidadeAcademiaId)
   {
      Comissao = comissao;
      PeriodoDeLideranca = periodoDeLideranca;
      AreaDeResponsabilidade = areaDeResponsabilidade;
      NivelDeLideranca = nivelDeLideranca;
   }
}