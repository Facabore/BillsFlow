namespace BillsFlow.Application.BillsDetails.Dtos;

public record BillDetailDto
{
    public Guid BillId { get; init; }
    public string Product { get; init; }
    public int Quantity { get; init; }
    public decimal UnitPrice { get; init; }
    public int TaxId { get; init; }

};