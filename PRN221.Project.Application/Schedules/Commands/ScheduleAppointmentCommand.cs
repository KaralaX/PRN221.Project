using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PRN221.Project.Application.Common.Interfaces;
using PRN221.Project.Domain.Entities;
using PRN221.Project.Domain.Enums;

namespace PRN221.Project.Application.Schedules.Commands;

public record ScheduleAppointmentCommand(
    Guid ServiceId,
    Guid DoctorId,
    DateTime ScheduledDateTime,
    Guid PatientId
) : IRequest<ErrorOr<ScheduleAppointmentResult>>;

public class ScheduleAppointmentCommandHandler : IRequestHandler<ScheduleAppointmentCommand, ErrorOr<ScheduleAppointmentResult>>
{
    private readonly IApplicationDbContext _dbContext;

    public ScheduleAppointmentCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ErrorOr<ScheduleAppointmentResult>> Handle(ScheduleAppointmentCommand request,
        CancellationToken token)
    {
        var schedules = _dbContext.Schedules;

        var schedule = await schedules.FirstOrDefaultAsync(
            s => s.DoctorId == request.DoctorId && s.ServiceId == request.ServiceId,
            token
        );

        if (schedule is null)
        {
            schedule = new Schedule
            {
                Id = Guid.NewGuid(),
                ServiceId = request.ServiceId,
                DoctorId = request.DoctorId,
                DateTime = request.ScheduledDateTime
            };

            schedules.Add(schedule);

            await _dbContext.SaveChangesAsync(token);
        }

        var appointment = new Appointment
        {
            Id = Guid.NewGuid(),
            PatientId = request.PatientId,
            ScheduledDateTime = request.ScheduledDateTime,
            Status = AppointmentStatus.Booked.ToString()
        };
        
        schedule.Appointments.Add(appointment);

        schedule.NumAppointment++;

        schedules.Update(schedule);
        
        await _dbContext.SaveChangesAsync(token);

        return new ScheduleAppointmentResult(
            appointment.Id,
            schedule.Doctor,
            appointment.Patient,
            request.ScheduledDateTime,
            schedule.Service.Name,
            schedule.Service.Price
        );
    }
}