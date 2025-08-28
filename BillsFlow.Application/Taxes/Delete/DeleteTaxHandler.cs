namespace BillsFlow.Application.Taxes.Delete;

#region Usings
using BillsFlow.Application.Abstractions.Messaging;
using BillsFlow.Domain.Abstractions;
using BillsFlow.Domain.Entities.Taxes;
#endregion

internal sealed class DeleteTaxHandler : ICommandHandler<DeleteTaxCommand>
{
    private readonly ITaxRepository _taxRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTaxHandler(
        ITaxRepository taxRepository,
        IUnitOfWork unitOfWork)
    {
        _taxRepository = taxRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        DeleteTaxCommand request, 
        CancellationToken cancellationToken)
    {
        var tax = await _taxRepository.GetByIntIdAsync(request.Id, cancellationToken);

        if(tax is null) return Result.Failure(TaxesErrors.NotFound);

        _taxRepository.Delete(tax);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}