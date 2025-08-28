namespace BillsFlow.Application.Taxes.Create;

#region Usings
using BillsFlow.Application.Abstractions.Messaging;
using BillsFlow.Domain.Abstractions;
using BillsFlow.Domain.Entities.Taxes;
#endregion

internal sealed class CreateTaxHandler : ICommandHandler<CreateTaxCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITaxRepository _taxRepository;

    public CreateTaxHandler(
        IUnitOfWork unitOfWork,
        ITaxRepository taxRepository)
    {
        _unitOfWork = unitOfWork;
        _taxRepository = taxRepository;
    }

    public async Task<Result> Handle(
        CreateTaxCommand request,
        CancellationToken cancellationToken)
    {
        
        var tax = Tax.Create(
            request.TaxDto.Name,
            request.TaxDto.Percentage);

        _taxRepository.Add(tax);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}