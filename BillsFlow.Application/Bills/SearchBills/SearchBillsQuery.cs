using BillsFlow.Application.Abstractions.Messaging;
using BillsFlow.Application.Abstractions.Pagination;
using BillsFlow.Application.Bills.Shared;

namespace BillsFlow.Application.Bills.SearchBills;

public sealed record SearchBillsQuery(
    string? SearchTerm,
    string? SortColumn,
    string? SortOrder,
    int Page,
    int PageSize) : IQuery<PagedList<BillResponse>>;