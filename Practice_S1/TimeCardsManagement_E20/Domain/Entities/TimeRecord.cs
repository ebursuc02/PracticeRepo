namespace TimeCardsManagement_E20.Domain.Entities;

public abstract class TimeRecord
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public DateOnly Date { get; protected set; }

    protected TimeRecord(DateOnly date) => Date = date;
}

public enum WorkLocation
{
    Office,
    Remote
}