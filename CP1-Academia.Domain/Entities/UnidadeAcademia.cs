using CP1_Academia.Domain.Common;
using CP1_Academia.Domain.Exceptions;

namespace CP1_Academia.Domain.Entities;

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

    public UnidadeAcademia(string telefone, bool ativo, DateTime horarioFuncionamento, Guid redeAcademiaId, Guid gerenteId, Guid localizacaoId)
    {
        if (string.IsNullOrWhiteSpace(telefone))
            throw new DomainException("O telefone da unidade é obrigatório.");

        Telefone = telefone;
        Ativo = ativo;
        HorarioFuncionamento = horarioFuncionamento;
        RedeAcademiaId = redeAcademiaId;
        GerenteId = gerenteId;
        LocalizacaoId = localizacaoId;
    }
}