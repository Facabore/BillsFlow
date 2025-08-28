namespace BillsFlow.Domain.Abstractions;

public interface ISoftDeletable
{
    public bool IsDeleted { get; }
    public DateTime? DeletedOnUtc { get; }
}