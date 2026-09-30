namespace CP1_Academia.API.Application.DTOs;

/// <summary>
/// Parâmetros de paginação já validados (page >= 1, pageSize entre 1 e 100).
/// </summary>
public sealed record PageRequest
{
    public const int DefaultPage = 1;
    public const int DefaultPageSize = 20;
    public const int MaxPageSize = 100;

    public int Page { get; }
    public int PageSize { get; }

    /// <summary>Quantidade de registros a pular (long para não estourar com page enorme).</summary>
    public long Offset => (long)(Page - 1) * PageSize;

    private PageRequest(int page, int pageSize)
    {
        Page = page;
        PageSize = pageSize;
    }

    /// <exception cref="ArgumentException">Quando page ou pageSize estão fora da regra.</exception>
    public static PageRequest Create(int page = DefaultPage, int pageSize = DefaultPageSize)
    {
        if (page < 1)
            throw new ArgumentException("O parâmetro 'page' deve ser um inteiro maior ou igual a 1.");

        if (pageSize < 1 || pageSize > MaxPageSize)
            throw new ArgumentException($"O parâmetro 'pageSize' deve estar entre 1 e {MaxPageSize}.");

        return new PageRequest(page, pageSize);
    }
}