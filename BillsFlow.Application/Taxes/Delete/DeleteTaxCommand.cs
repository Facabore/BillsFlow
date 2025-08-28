namespace BillsFlow.Application.Taxes.Delete;

using BillsFlow.Application.Abstractions.Messaging;

public sealed record DeleteTaxCommand(int Id) : ICommand;