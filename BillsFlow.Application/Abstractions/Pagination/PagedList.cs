using Microsoft.EntityFrameworkCore;

namespace BillsFlow.Application.Abstractions.Pagination;

public class PagedList<T>
{
    private PagedList(List<T> data, PaginationDetails paginationDetails)
    {
        Data = data;
        PaginationDetails = paginationDetails;
    }

    public List<T> Data { get; }

    public PaginationDetails PaginationDetails { get; }

    public static async Task<PagedList<T>> CreateAsync(IQueryable<T> query, int page, int pageSize)
    {
        var totalItems = await query.CountAsync();
        var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

        var pagination = new PaginationDetails(
            CurrentPage: page,
            PageSize: pageSize,
            TotalItems: totalItems,
            TotalPages: totalPages,
            HasNextPage: page * pageSize < totalItems,
            HasPreviousPage: page > 1
        );

        var data = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        return new(data, pagination);
    }
}