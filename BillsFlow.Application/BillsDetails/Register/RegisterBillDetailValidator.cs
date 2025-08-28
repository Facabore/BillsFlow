using FluentValidation;

namespace BillsFlow.Application.BillsDetails.Register;

public class RegisterBillDetailValidator : AbstractValidator<RegisterBillDetailCommand>
{
    #region Constants

    private const int MaxDefaultSize = 100;
    private const int Zero = 0;
    #endregion
    public RegisterBillDetailValidator()
    {
        RuleFor(bd => bd.BillDetailDto.Product)
            .NotEmpty()
            .MaximumLength(MaxDefaultSize);

        RuleFor(bd => bd.BillDetailDto.Quantity)
            .NotEmpty()
            .GreaterThan(Zero);

        RuleFor(bd => bd.BillDetailDto.UnitPrice)
            .NotEmpty()
            .GreaterThan(Zero);

        RuleFor(bd => bd.BillDetailDto.TaxId)
            .NotEmpty();
    }
}