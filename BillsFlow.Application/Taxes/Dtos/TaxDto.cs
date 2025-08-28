namespace BillsFlow.Application.Taxes.Dtos;

public record TaxDto
{
    public string Name { get; init; }
    public decimal Percentage { get; init; }
};