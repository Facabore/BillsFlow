using System.Text.RegularExpressions;

namespace BillsFlow.Domain.Entities.Customers.ValueObjects;

public partial record PhoneNumber
{
    // The phone number must start with a +
    // The phone number must have at least 7 digits and at most 15 digits (excluding the +)
    private const string PhoneNumberPattern = @"^\+[1-9]{1}[0-9]{6,14}$";
    public string Value { get; }

    private PhoneNumber(string value)
    {
        Value = value;
    }

    public static PhoneNumber Create(string phoneNumber)
    {
        if (!NumberPhoneRegex().IsMatch(phoneNumber))
        {
            throw new ArgumentException("Invalid phone number");
        }

        return new PhoneNumber(phoneNumber);
    }

    [GeneratedRegex(PhoneNumberPattern)]
    private static partial Regex NumberPhoneRegex();
}