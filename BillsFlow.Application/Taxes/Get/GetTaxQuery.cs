using BillsFlow.Application.Abstractions.Messaging;
using BillsFlow.Application.Taxes.Shared;

namespace BillsFlow.Application.Taxes.Get;

public record GetTaxQuery() : ICommand<IEnumerable<TaxResponse>>;