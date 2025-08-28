using BillsFlow.Application.Abstractions.Messaging;

namespace BillsFlow.Application.BillsDetails.Delete;

public sealed record DeleteBillDetailCommand(Guid BillId, Guid DetailId) : ICommand;