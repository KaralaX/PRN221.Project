using FluentValidation;
using PRN221.Project.Application.Common.Interfaces;

namespace PRN221.Project.Application.Schedules.Commands;

public class ScheduleAppointmentCommandValidator : AbstractValidator<ScheduleAppointmentCommand>
{
    public ScheduleAppointmentCommandValidator()
    {
        RuleFor(x => x.ScheduledDateTime).NotEmpty();
        RuleFor(x => x.PatientId).NotEmpty();
        RuleFor(x => x.DoctorId).NotEmpty();
        RuleFor(x => x.ServiceId).NotEmpty();
    }
}