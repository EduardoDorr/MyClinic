using MyClinic.Common.DomainEvents;

namespace MyClinic.Common.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; }
    public DateTime CreatedAt { get; }
    public int Year { get; }
    public int Month { get; }
    public int Day { get; }
    public DateTime UpdatedAt { get; protected set; }
    public bool Active { get; protected set; }

    private readonly List<IDomainEvent> _domainEvents = [];

    protected BaseEntity()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;

        Year = CreatedAt.Year;
        Month = CreatedAt.Month;
        Day = CreatedAt.Day;

        Active = true;
    }

    public virtual void Activate()
        => Active = true;

    public virtual void Deactivate()
        => Active = false;

    public virtual void SetUpdatedAtDate(DateTime updatedAtDate)
        => UpdatedAt = updatedAtDate;

    public IReadOnlyList<IDomainEvent> GetDomainEvents()
        => _domainEvents.ToList();

    public void ClearDomainEvents()
        => _domainEvents.Clear();

    protected void RaiseDomainEvent(IDomainEvent domainEvent)
        => _domainEvents.Add(domainEvent);
}