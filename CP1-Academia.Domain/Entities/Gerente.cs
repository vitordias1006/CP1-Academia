namespace ClassLibrary1.Entities;

public class Gerente : Funcionario
{
   public double Comissao { get; private set; }
   
   public DateTime PeriodoDeLideranca { get; private set; }
   
   public String AreaDeResponsabilidade { get; private set; }
   
   public String NivelDeLideranca { get; private set; }
   
   
   
   public List<UnidadeAcademia> UnidadesAcademia { get; private set; }

   public Gerente(string nome, string cpf, string email, string cargo, double salario, DateTime dataDeContratacao, bool ativo, double comissao, DateTime periodoDeLideranca, string areaDeResponsabilidade, string nivelDeLideranca, List<UnidadeAcademia> unidadesAcademia) : base(nome, cpf, email, cargo, salario, dataDeContratacao, ativo)
   {
      Comissao = comissao;
      PeriodoDeLideranca = periodoDeLideranca;
      AreaDeResponsabilidade = areaDeResponsabilidade;
      NivelDeLideranca = nivelDeLideranca;
      UnidadesAcademia = unidadesAcademia;
   }
}