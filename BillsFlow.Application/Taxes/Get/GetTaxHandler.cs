using BillsFlow.Application.Abstractions.Messaging;
using BillsFlow.Application.Taxes.Shared;
using BillsFlow.Domain.Abstractions;
using BillsFlow.Domain.Entities.Taxes;

namespace BillsFlow.Application.Taxes.Get;

internal sealed class GetTaxHandler : ICommandHandler<GetTaxQuery, IEnumerable<TaxResponse>>
{
    private readonly ITaxRepository _taxRepository;

    public GetTaxHandler(ITaxRepository taxRepository)
    {
        _taxRepository = taxRepository;
    }

    public async Task<Result<IEnumerable<TaxResponse>>> Handle(
        GetTaxQuery request,
        CancellationToken cancellationToken)
    {
        var taxes = await _taxRepository.GetAllAsync(cancellationToken);

        var response = taxes
            .Select(t => new TaxResponse
            {
                Id = t.Id,
                Name = t.Name,
                Percentage = t.Percentage
            });

        return Result.Success(response);
    }
}