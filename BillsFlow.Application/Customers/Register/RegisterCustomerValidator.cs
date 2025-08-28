namespace BillsFlow.Application.Customers.Register;

#region Usings
using BillsFlow.Domain.Entities.Customers.ValueObjects;
using FluentValidation;
#endregion

public sealed class RegisterCustomerValidator : AbstractValidator<RegisterCustomerCommand>
{
    #region Constants
    private const int MaxLengthNames = 50;
    private const string RegexDocumentNumber = @"^\d{6,10}$";
    private const string RegexPhoneNumber = @"^\+[1-9][0-9]{6,14}$";
    #endregion
    public RegisterCustomerValidator()
    {
        RuleFor(c => c.CustomerDto.FirstName)
            .NotEmpty()
            .MaximumLength(MaxLengthNames);

        RuleFor(c => c.CustomerDto.LastName)
            .NotEmpty()
            .MaximumLength(MaxLengthNames);

        RuleFor(c => c.CustomerDto.Email)
            .EmailAddress()
            .WithMessage("The email has not the correct format");

        RuleFor(c => c.CustomerDto.Gender)
            .Must(BeAValidGender)
            .WithMessage("Invalid gender. Valid options: male, female, other");

        RuleFor(c => c.CustomerDto.PhoneNumber)
            .Matches(RegexPhoneNumber);

        RuleFor(c => c.CustomerDto.DocumentType)
            .Must(BeAValidDocumentType)
            .WithMessage("Invalid document type. Valid options: cc, passport.");

        RuleFor(c => c.CustomerDto.DocumentNumber)
            .Matches(RegexDocumentNumber)
            .WithMessage("The document number is not correct");
    }

    private bool BeAValidGender(string value)
    {
        return Gender.All.Any(g => g.Value.Equals(value, StringComparison.OrdinalIgnoreCase));
    }

    private bool BeAValidDocumentType(string value)
    {
        return DocumentType.All.Any(d => d.Value.Equals(value, StringComparison.OrdinalIgnoreCase));
    }
}