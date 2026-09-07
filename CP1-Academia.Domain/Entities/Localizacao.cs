using CP1_Academia.Domain.Common;
using CP1_Academia.Domain.Exceptions;

namespace CP1_Academia.Domain.Entities;

public class Localizacao : BaseEntity
{
    public string Estado { get; private set; }
    
    public string Cidade { get; private set; }
    
    public string Bairro { get; private set; }
    
    public string Cep { get; private set; }
    
    public string Rua { get; private set; }
    
    public int Numero { get; private set; }
    
    public Guid UnidadeAcademiaId { get; private set; }
    public UnidadeAcademia UnidadeAcademia { get; private set; }

    public Localizacao(string estado, string cidade, string bairro, string cep, string rua, int numero, Guid unidadeAcademiaId)
    {
        if (string.IsNullOrWhiteSpace(estado))
            throw new DomainException("O estado é obrigatório.");

        if (string.IsNullOrWhiteSpace(cep))
            throw new DomainException("O CEP é obrigatório.");

        Estado = estado;
        Cidade = cidade;
        Bairro = bairro;
        Cep = cep;
        Rua = rua;
        Numero = numero;
        UnidadeAcademiaId = unidadeAcademiaId;
    }
}