namespace BillsFlow.Application.Bills.Delete;

using BillsFlow.Application.Abstractions.Messaging;

public sealed record DeleteBillCommand(Guid BillId) : ICommand;