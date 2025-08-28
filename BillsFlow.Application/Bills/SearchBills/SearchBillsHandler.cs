using BillsFlow.Application.Abstractions.Messaging;
using BillsFlow.Application.Abstractions.Pagination;
using BillsFlow.Application.Bills.Shared;
using BillsFlow.Application.BillsDetails.Shared;
using BillsFlow.Domain.Abstractions;
using BillsFlow.Domain.Entities.Bills;
using System.Linq.Expressions;

namespace BillsFlow.Application.Bills.SearchBills;

internal sealed class SearchBillsQueryHandler
    : IQueryHandler<SearchBillsQuery, PagedList<BillResponse>>
{
    private readonly IBillRepository _billRepository;

    public SearchBillsQueryHandler(IBillRepository billRepository)
    {
        _billRepository = billRepository;
    }

    public async Task<Result<PagedList<BillResponse>>> Handle(
        SearchBillsQuery request,
        CancellationToken cancellationToken)
    {
        IQueryable<Bill> billsQuery = _billRepository.GetQueryable();

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            string searchTerm = request.SearchTerm.ToLower();
            billsQuery = billsQuery.Where(b =>
                b.Concept.Value.ToLower().Contains(searchTerm));
        }

        if (request.SortOrder?.ToLower() == "desc")
        {
            billsQuery = billsQuery.OrderByDescending(GetSortProperty(request));
        }
        else
        {
            billsQuery = billsQuery.OrderBy(GetSortProperty(request));
        }

        var billResponsesQuery = billsQuery.Select(b => new BillResponse(
            b.Id,
            b.CustomerId,
            b.Concept.Value,
            b.TotalPrice.Value,
            b.Status.ToString(),
            b.IssueDate,
            new List<BillDetailResponse>() 
        ));

        var bills = await PagedList<BillResponse>.CreateAsync(
            billResponsesQuery,
            request.Page,
            request.PageSize);

        return bills;
    }

  
    private static Expression<Func<Bill, object>> GetSortProperty(SearchBillsQuery request)
    {
        return request.SortColumn?.ToLower() switch
        {
            "concept" => bill => bill.Concept.Value,
            "total" => bill => bill.TotalPrice.Value,
            "date" => bill => bill.IssueDate,
            _ => bill => bill.Id
        };
    }
}