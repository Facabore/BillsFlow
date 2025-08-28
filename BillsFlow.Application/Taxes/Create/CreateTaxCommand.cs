namespace BillsFlow.Application.Taxes.Create;

using BillsFlow.Application.Abstractions.Messaging;

public record CreateTaxCommand(Dtos.TaxDto TaxDto) : ICommand;