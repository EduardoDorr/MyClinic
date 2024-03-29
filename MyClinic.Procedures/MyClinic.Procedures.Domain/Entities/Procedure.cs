using MyClinic.Common.Results;
using MyClinic.Common.Entities;
using MyClinic.Common.DomainEvents;

namespace MyClinic.Procedures.Domain.Entities;

public class Procedure : BaseEntity
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Cost { get; private set; }
    public int Duration { get; private set; }
    public int MinimumSchedulingNotice { get; private set; }
    public Guid SpecialityId { get; private set; }

    private Procedure(string name, string description, decimal cost, int duration, int minimumSchedulingNotice, Guid specialityId)
    {
        Name = name;
        Description = description;
        Cost = cost;
        Duration = duration;
        MinimumSchedulingNotice = minimumSchedulingNotice;
        SpecialityId = specialityId;
    }

    public static Result<Procedure> Create(string name, string description, decimal cost, int duration, int minimumSchedulingNotice, Guid specialityId)
    {
        var procedure =
            new Procedure(name,
                          description,
                          cost,
                          duration,
                          minimumSchedulingNotice,
                          specialityId);

        return Result.Ok(procedure);
    }

    public void Update(string name, string description, decimal cost, int duration, int minimumSchedulingNotice, Guid specialityId)
    {
        Name = name;
        Description = description;
        Cost = cost;
        Duration = duration;
        MinimumSchedulingNotice = minimumSchedulingNotice;
        SpecialityId = specialityId;
    }

    public void RaiseEvent(IDomainEvent domainEvent) =>
        RaiseDomainEvent(domainEvent);
}