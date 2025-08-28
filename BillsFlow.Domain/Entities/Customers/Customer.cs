namespace BillsFlow.Domain.Entities.Customers;

#region Usings
using BillsFlow.Domain.Abstractions;
using BillsFlow.Domain.Entities.Customers.ValueObjects;
#endregion

public sealed class Customer : Entity
{
    private Customer(
        Guid id,
        PersonalInfo personalInfo,
        ContactInfo contactInfo,
        DateTime createdAt)
        : base(id)
    {
        PersonalInfo = personalInfo;
        ContactInfo = contactInfo;
        CreatedAt = createdAt;
    }

    private Customer()
    {

    }

    public PersonalInfo PersonalInfo { get; private set; }
    public ContactInfo ContactInfo { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public static Customer Create(
        PersonalInfo personalInfo,
        ContactInfo contactInfo,
        DateTime createdAt)
    {
        var customer = new Customer(
            Guid.NewGuid(),
            personalInfo,
            contactInfo,
            createdAt);

        return customer;
    }

    public void Update(
        PersonalInfo personalInfo,
        ContactInfo contactInfo)
    {
        PersonalInfo = personalInfo;
        ContactInfo = contactInfo;
    }
}