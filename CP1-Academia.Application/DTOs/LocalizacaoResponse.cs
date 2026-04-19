using CP1_Academia.Domain.Entities;

namespace CP1_Academia.API.Application.DTOs;

public record LocalizacaoResponse(
    Guid Id,
    string Estado,
    string Cidade,
    string Bairro,
    string Cep,
    string Rua,
    int Numero,
    Guid UnidadeAcademiaId)
{
    public static LocalizacaoResponse FromDomain(Localizacao localizacao) => new(localizacao.Id, localizacao.Estado, localizacao.Cidade, localizacao.Bairro, localizacao.Cep, localizacao.Rua, localizacao.Numero, localizacao.UnidadeAcademiaId);
}