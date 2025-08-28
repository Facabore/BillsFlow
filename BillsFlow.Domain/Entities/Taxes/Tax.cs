using BillsFlow.Domain.Abstractions;

namespace BillsFlow.Domain.Entities.Taxes;

public sealed class Tax : Entity
{
    private Tax() { }

    private Tax(
        string name,
        decimal percentage)
    {
        Name = name;
        Percentage = percentage;
    }

    public int Id { get; private set; }
    public string Name { get; private set; }
    public decimal Percentage { get; private set; }

    public static Tax Create(
        string name, 
        decimal percentage)
    {
        const int Zero = 0;
        if(percentage < Zero)
            throw new ArgumentOutOfRangeException(nameof(percentage), "The percentage must be greater than or equal to zero.");

        var tax = new Tax
        (
            name,
            percentage
        );

        return tax;
    }

    public static Tax CreateForSeeding(
        int id,
        string name,
        decimal percentage)
    {
        var tax = new Tax(
            name,
            percentage
        )
        {
            Id = id
        };

        return tax;
    }
}