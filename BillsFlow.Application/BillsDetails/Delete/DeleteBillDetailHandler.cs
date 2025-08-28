namespace BillsFlow.Application.BillsDetails.Delete;

#region Usings
using BillsFlow.Application.Abstractions.Messaging;
using BillsFlow.Domain.Abstractions;
using BillsFlow.Domain.Entities.Bills;
#endregion


internal sealed class DeleteBillDetailHandler : ICommandHandler<DeleteBillDetailCommand>
{
    private readonly IBillRepository _billRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBillDetailRepository _billDetailRepository;

    public DeleteBillDetailHandler(
        IBillRepository billRepository,
        IUnitOfWork unitOfWork,
        IBillDetailRepository billDetailRepository)
    {
        _billRepository = billRepository;
        _unitOfWork = unitOfWork;
        _billDetailRepository = billDetailRepository;
    }

    public async Task<Result> Handle(
        DeleteBillDetailCommand request,
        CancellationToken cancellationToken)
    {
        var bill = await _billRepository.GetByIdAsync(request.BillId, cancellationToken);
        if (bill is null) return Result.Failure(BillErrors.NotFound);

        var billDetail = _billDetailRepository.GetByIdAsync(request.DetailId, cancellationToken);
        if (billDetail is null) return Result.Failure(BillDetailErrors.NotFound);

        bill.RemoveDetail(request.DetailId);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();

    }
}