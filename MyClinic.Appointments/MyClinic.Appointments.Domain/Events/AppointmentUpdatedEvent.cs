﻿using MyClinic.Common.DomainEvents;

using MyClinic.Appointments.Domain.Events.Shared;

namespace MyClinic.Appointments.Domain.Events;

public sealed record AppointmentUpdatedEvent(
    Guid AppointmentId,
    PersonEvent Patient,
    PersonEvent Doctor,
    ProcedureEvent Procedure) : IDomainEvent;