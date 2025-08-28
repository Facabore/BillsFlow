namespace BillsFlow.Application.Customers.SearchCustomers;

#region Usings
using BillsFlow.Application.Abstractions.Messaging;
using BillsFlow.Application.Abstractions.Pagination;
using BillsFlow.Application.Customers.Shared;
#endregion

public record SearchCustomersQuery(
    string? SearchParam,
    string? SortColumn,
    string? SortOrder,
    int Page,
    int PageSize) : IQuery<PagedList<CustomerResponse>>;