using BillsFlow.Application.Abstractions.Messaging;
using BillsFlow.Application.Bills.Shared;

namespace BillsFlow.Application.Bills.GetById;

public record GetBillByIdQuery(Guid BillId) : IQuery<BillResponse>;