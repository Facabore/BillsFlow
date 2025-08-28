namespace BillsFlow.Application.Customers.Shared;

public record CustomerWithoutFilterResponse
{
    public Guid CustomerId { get; init; }
    public string Name { get; init; }
    public string Gender { get; init; }
    public string DocumentType { get; init; }
    public string DocumentNumber { get; init; }
    public string Email { get; init; }
    public string PhoneNumber { get; init; }
    public DateTime CreatedOnUtc { get; init; }
    public bool IsDeleted { get; init; }
};