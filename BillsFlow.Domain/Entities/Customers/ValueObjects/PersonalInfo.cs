namespace BillsFlow.Domain.Entities.Customers.ValueObjects;

public sealed class PersonalInfo(
    string firstName,
    string lastName,
    Gender gender,
    DocumentType documentType,
    string documentNumber)
{
    public string FirstName { get; private set; } = firstName;

    public string LastName { get; private set; } = lastName;

    public Gender Gender { get; private set; } = gender;

    public DocumentType DocumentType { get; private set; } = documentType;

    public string DocumentNumber { get; private set; } = documentNumber;
}