using BillsFlow.Application.Abstractions.Messaging;
using BillsFlow.Application.BillsDetails.Shared;
using BillsFlow.Domain.Abstractions;
using BillsFlow.Domain.Entities.Bills;

namespace BillsFlow.Application.BillsDetails.GetBillDetails;

#region Usings



#endregion

internal sealed class GetBillDetailHandler : IQueryHandler<GetBillDetailsQuery, IEnumerable<BillDetailResponse>>
{
    private readonly IBillRepository _billRepository;

    public GetBillDetailHandler(IBillRepository billRepository)
    {
        _billRepository = billRepository;
    }

    public async Task<Result<IEnumerable<BillDetailResponse>>> Handle(
        GetBillDetailsQuery request, 
        CancellationToken cancellationToken)
    {
        var bill = await _billRepository.GetByIdWithIncludeAsync(request.BillId, cancellationToken);
        if (bill is null) return Result.Failure<IEnumerable<BillDetailResponse>>(BillErrors.NotFound);

        var billDetails = bill.Details
            .Select(detail => new BillDetailResponse
            {
                Id = detail.Id,
                BillId = detail.BillId,
                Product = detail.Product.Name,
                Quantity = detail.Quantity.Value,
                UnitPrice = detail.UnitPrice.Value,
                TaxRateId = detail.TaxId
            })
            ;

        return Result.Success(billDetails);
    }
}