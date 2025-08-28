namespace BillsFlow.Infrastructure.Clock;

using BillsFlow.Application.Abstractions.Clock;

internal sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}