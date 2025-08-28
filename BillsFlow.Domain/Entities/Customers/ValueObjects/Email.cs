using System.Text.RegularExpressions;

namespace BillsFlow.Domain.Entities.Customers.ValueObjects;

public partial record Email
{
    // Basic email pattern for validation
    private const string EmailPattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";

    public string Value { get; }

    private Email(string value)
    {
        Value = value;
    }

    public static Email Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email) || !EmailRegex().IsMatch(email))
        {
            throw new ArgumentException("Invalid email address");
        }

        return new Email(email);
    }

    [GeneratedRegex(EmailPattern)]
    private static partial Regex EmailRegex();
}