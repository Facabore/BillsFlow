using BillsFlow.Application.Abstractions.Messaging;
using BillsFlow.Application.Bills.Dtos;

namespace BillsFlow.Application.Bills.Create;

public sealed record CreateBillCommand(BillDto BillDto) : ICommand<Guid>;