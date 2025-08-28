namespace BillsFlow.Application.BillsDetails.Shared;

public sealed record BillDetailResponse
{
    public Guid Id { get; init; }
    public Guid BillId { get; init; }
    public string Product { get; init; }
    public int Quantity { get; init; }
    public decimal UnitPrice { get; init; }
    public int TaxRateId { get; init; }
}