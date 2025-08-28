namespace BillsFlow.Application.Customers.Dtos;

public record CustomerDto
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public string PhoneNumber { get; init; }
    public string Gender { get; init; }
    public string DocumentType { get; init; }
    public string DocumentNumber { get; init; }
}