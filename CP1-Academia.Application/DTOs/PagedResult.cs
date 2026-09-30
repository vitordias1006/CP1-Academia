namespace CP1_Academia.API.Application.DTOs;

public sealed record PagedResult<T>(
    int Page,
    int PageSize,
    int TotalItems,
    int TotalPages,
    IReadOnlyList<T> Items)
{
    // (Recomendado no enunciado)
    public bool HasPrevious => Page > 1;
    public bool HasNext => Page < TotalPages;

    public static PagedResult<T> Create(PageRequest request, int totalItems, IReadOnlyList<T> items)
    {
        // totalPages = teto de totalItems / pageSize (pageSize já foi validado >= 1)
        var totalPages = (int)Math.Ceiling(totalItems / (double)request.PageSize);
        return new PagedResult<T>(request.Page, request.PageSize, totalItems, totalPages, items);
    }
}