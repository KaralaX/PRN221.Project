using PRN221.Project.Domain.Entities;

namespace PRN221.Project.Application.Schedules.Commands;

public record ScheduleAppointmentResult(
    Guid AppointmentId,
    Doctor Doctor,
    Patient Patient,
    DateTime ScheduledDateTime,
    string Service,
    decimal Price
);