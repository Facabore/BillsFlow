namespace BillsFlow.Application.Bills.Update;

#region Usings
using BillsFlow.Application.Bills.Dtos;
using BillsFlow.Application.Abstractions.Messaging;
using BillsFlow.Domain.Entities.Bills.ValueObjects;
#endregion

public sealed record UpdateBillCommand(
    Guid BillId,
    BillDto BillDto,
    BillStatus Status) : ICommand;