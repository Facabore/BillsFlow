namespace BillsFlow.Application.Bills.Dtos;

public record BillDto
{
    public Guid CustomerId { get; init; }
    public string Concept { get; init; }
    public DateTime IssueData { get; init; }
}