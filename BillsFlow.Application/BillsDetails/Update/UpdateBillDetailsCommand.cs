using BillsFlow.Application.Abstractions.Messaging;

namespace BillsFlow.Application.BillsDetails.Update;

public record UpdateBillDetailsCommand(Guid Id, Dtos.BillDetailDto BillDetailDto) : ICommand;