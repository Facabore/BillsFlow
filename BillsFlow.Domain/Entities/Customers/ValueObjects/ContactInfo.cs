namespace BillsFlow.Domain.Entities.Customers.ValueObjects;

public sealed class ContactInfo(
    PhoneNumber phoneNumber,
    Email email)
{
    public PhoneNumber PhoneNumber { get; private set; } = phoneNumber;
    public Email Email { get; private set; } = email;

}
