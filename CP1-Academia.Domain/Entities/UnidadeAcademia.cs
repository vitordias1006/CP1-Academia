using ClassLibrary1.Common;

namespace ClassLibrary1.Entities;

public class UnidadeAcademia : BaseEntity
{
    public string Telefone { get; private set; }
    
    public bool Ativo { get; private set; }
    
    public DateTime HorarioFuncionamento { get; private set; }
    
    public Guid RedeAcademiaId { get; private set; }
    public RedeAcademia RedeAcademia { get; private set; }
    
    public Guid GerenteId { get; private set; }
    public Gerente Gerente { get; private set; }
    
    public List<Funcionario> Funcionarios { get; private set; }
    
    public Guid LocalizacaoId { get; private set; }
    public Localizacao Localizacao { get; private set; }

    public UnidadeAcademia(string telefone, bool ativo, DateTime horarioFuncionamento, Guid redeAcademiaId, RedeAcademia redeAcademia, Guid gerenteId, Gerente gerente, List<Funcionario> funcionarios, Guid localizacaoId, Localizacao localizacao)
    {
        Telefone = telefone;
        Ativo = ativo;
        HorarioFuncionamento = horarioFuncionamento;
        RedeAcademiaId = redeAcademiaId;
        RedeAcademia = redeAcademia;
        GerenteId = gerenteId;
        Gerente = gerente;
        Funcionarios = funcionarios;
        LocalizacaoId = localizacaoId;
        Localizacao = localizacao;
    }
}