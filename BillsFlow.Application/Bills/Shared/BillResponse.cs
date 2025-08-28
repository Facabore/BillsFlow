using BillsFlow.Application.BillsDetails.Shared;

namespace BillsFlow.Application.Bills.Shared;

public sealed record BillResponse(
    Guid Id,
    Guid CustomerId,
    string Concept,
    decimal TotalPrice,
    string Status,
    DateTime IssueDate,
    List<BillDetailResponse> Details);