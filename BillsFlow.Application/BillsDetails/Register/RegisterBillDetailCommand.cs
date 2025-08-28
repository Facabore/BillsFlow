using BillsFlow.Application.Abstractions.Messaging;

namespace BillsFlow.Application.BillsDetails.Register;

public record RegisterBillDetailCommand(Dtos.BillDetailDto BillDetailDto) : ICommand<Guid>;