namespace BillsFlow.Domain.Abstractions;

public abstract class Entity : ISoftDeletable
{
    protected Entity(Guid id)
    {
        Id = id;
    }

    protected Entity()
    {

    }
    public Guid Id { get; init; }


    public bool IsDeleted { get; private set; }
    public DateTime? DeletedOnUtc { get; private set; }
    public void Delete()
    {
        IsDeleted = true;
        DeletedOnUtc = DateTime.UtcNow;
    }
}
