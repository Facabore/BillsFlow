namespace BillsFlow.Application.Taxes.Create;

using FluentValidation;

public sealed class CreateTaxValidator : AbstractValidator<CreateTaxCommand>
{
    #region Constants
    private const int NameMaxLength = 100;
    private const int MaxPercentage = 100;
    private const int MinPercentage = 0;

    #endregion
    public CreateTaxValidator()
    {
        RuleFor(t => t.TaxDto.Name)
            .NotEmpty()
            .MaximumLength(NameMaxLength);

        RuleFor(t => t.TaxDto.Percentage)
            .InclusiveBetween(MinPercentage, MaxPercentage);
    }
}