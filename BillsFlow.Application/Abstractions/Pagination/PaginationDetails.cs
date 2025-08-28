namespace BillsFlow.Application.Abstractions.Pagination;

public record PaginationDetails(
    int CurrentPage,
    int PageSize,
    int TotalItems,
    int TotalPages,
    bool HasNextPage,
    bool HasPreviousPage);