namespace BillsFlow.Application.Taxes.Shared;

public record TaxResponse
{
    public int Id { get; init; }
    public string Name { get; init; }
    public decimal Percentage { get; init; }
};