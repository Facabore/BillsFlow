namespace BillsFlow.Application.Bills.Update;

#region Usings
using BillsFlow.Application.Abstractions.Messaging;
using BillsFlow.Domain.Abstractions;
using BillsFlow.Domain.Entities.Bills;
using BillsFlow.Domain.Entities.Bills.ValueObjects;
#endregion

internal sealed class UpdateBillHandler : ICommandHandler<UpdateBillCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBillRepository _billRepository;

    public UpdateBillHandler(
        IUnitOfWork unitOfWork,
        IBillRepository billRepository)
    {
        _unitOfWork = unitOfWork;
        _billRepository = billRepository;
    }

    public async Task<Result> Handle(
        UpdateBillCommand request,
        CancellationToken cancellationToken)
    {
        var bill = await _billRepository.GetByIdAsync(request.BillId, cancellationToken);
        if (bill is null) return Result.Failure(BillErrors.NotFound);

        bill.Update(
            new Concept(request.BillDto.Concept),
            request.BillDto.IssueData,
            request.Status
        );

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}