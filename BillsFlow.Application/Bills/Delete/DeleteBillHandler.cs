using BillsFlow.Application.Abstractions.Messaging;
using BillsFlow.Domain.Abstractions;
using BillsFlow.Domain.Entities.Bills;

namespace BillsFlow.Application.Bills.Delete;

internal sealed class DeleteBillHandler : ICommandHandler<DeleteBillCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBillRepository _billRepository;

    public DeleteBillHandler(
        IUnitOfWork unitOfWork,
        IBillRepository billRepository)
    {
        _unitOfWork = unitOfWork;
        _billRepository = billRepository;
    }

    public async Task<Result> Handle(
        DeleteBillCommand request, 
        CancellationToken cancellationToken)
    {
        var bill = await _billRepository.GetByIdAsync(request.BillId, cancellationToken);
        if (bill is null) return Result.Failure(BillErrors.NotFound);

        bill.Delete();

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}