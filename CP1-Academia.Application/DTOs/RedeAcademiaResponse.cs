using CP1_Academia.Domain.Entities;

namespace CP1_Academia.API.Application.DTOs;

public record RedeAcademiaResponse(
    Guid Id,
    string Nome,
    int QntdUnidades,
    string Cnpj,
    DateTime DataFundacao)
{
    public static RedeAcademiaResponse FromDomain(RedeAcademia redeAcademia) => new(redeAcademia.Id, redeAcademia.Nome, redeAcademia.QntdUnidades, redeAcademia.Cnpj, redeAcademia.DataFundacao);
}