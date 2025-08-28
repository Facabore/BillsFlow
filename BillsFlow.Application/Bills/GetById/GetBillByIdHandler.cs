using BillsFlow.Application.Abstractions.Messaging;
using BillsFlow.Application.Bills.Shared;
using BillsFlow.Application.BillsDetails.Shared;
using BillsFlow.Domain.Abstractions;
using BillsFlow.Domain.Entities.Bills;

namespace BillsFlow.Application.Bills.GetById;

internal sealed class GetBillByIdQueryHandler : IQueryHandler<GetBillByIdQuery, BillResponse>
{
    private readonly IBillRepository _billRepository;

    public GetBillByIdQueryHandler(IBillRepository billRepository)
    {
        _billRepository = billRepository;
    }

    public async Task<Result<BillResponse>> Handle(GetBillByIdQuery request, CancellationToken cancellationToken)
    {
        var bill = await _billRepository.GetByIdWithIncludeAsync(request.BillId, cancellationToken);

        if (bill is null) return Result.Failure<BillResponse>(BillErrors.NotFound);

        var response = new BillResponse(
            bill.Id,
            bill.CustomerId,
            bill.Concept.Value,
            bill.TotalPrice.Value,
            bill.Status.ToString(),
            bill.IssueDate,
            bill.Details.Select(d => new BillDetailResponse
            {
                Id = d.Id,
                BillId = d.BillId,
                Product = d.Product.Name,
                Quantity = d.Quantity.Value,
                UnitPrice = d.UnitPrice.Value,
                TaxRateId = d.TaxId
            }).ToList()
        );

        return response;
    }
}