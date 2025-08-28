using BillsFlow.Application.Abstractions.Messaging;
using BillsFlow.Domain.Abstractions;
using BillsFlow.Domain.Entities.Bills;
using BillsFlow.Domain.Entities.Bills.ValueObjects;
using BillsFlow.Domain.Entities.Taxes;

namespace BillsFlow.Application.BillsDetails.Update;

#region Usings



#endregion

internal sealed class UpdateBillDetailsHandler : ICommandHandler<UpdateBillDetailsCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBillDetailRepository _billDetailRepository;
    private readonly ITaxRepository _taxRepository;
    private readonly IBillRepository _billRepository;

    public UpdateBillDetailsHandler(
        IUnitOfWork unitOfWork,
        IBillDetailRepository billDetailRepository,
        ITaxRepository taxRepository,
        IBillRepository billRepository)
    {
        _unitOfWork = unitOfWork;
        _billDetailRepository = billDetailRepository;
        _taxRepository = taxRepository;
        _billRepository = billRepository;
    }

    public async Task<Result> Handle(
        UpdateBillDetailsCommand request,
        CancellationToken cancellationToken)
    {
        var bill = await _billRepository.GetByIdAsync(request.BillDetailDto.BillId, cancellationToken);
        if (bill is null) return Result.Failure(BillErrors.NotFound);

        var billDetail = await _billDetailRepository.GetByIdAsync(request.Id, cancellationToken);
        if (billDetail is null) return Result.Failure(BillDetailErrors.NotFound);

        var tax = await _taxRepository.GetByIntIdAsync(request.BillDetailDto.TaxId, cancellationToken);
        if (tax is null) return Result.Failure(TaxesErrors.NotFound);

        bill.UpdateDetail(
            request.Id,
            request.BillDetailDto.Product,
            request.BillDetailDto.Quantity,
            request.BillDetailDto.UnitPrice,
            request.BillDetailDto.TaxId);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}