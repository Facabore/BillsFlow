namespace BillsFlow.Application.BillsDetails.GetBillDetails;

#region Usings
using BillsFlow.Application.Abstractions.Messaging;
using BillsFlow.Application.BillsDetails.Shared;
#endregion


public sealed record GetBillDetailsQuery(Guid BillId) : IQuery<IEnumerable<BillDetailResponse>>;