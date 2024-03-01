using MediatR;
using Microsoft.EntityFrameworkCore;
using PRN221.Project.Application.Common.Interfaces;
using PRN221.Project.Domain.Enums;

namespace PRN221.Project.Application.Appointments.Commands;

public record StartAppointmentCommand(Guid AppointmentId) : IRequest;

public class StartAppointmentCommandHandler : IRequestHandler<StartAppointmentCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public StartAppointmentCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(StartAppointmentCommand request, CancellationToken token)
    {
        var appointments = _dbContext.Appointments;

        var appointment = await appointments.FirstOrDefaultAsync(a => a.Id == request.AppointmentId, cancellationToken: token);

        if (appointment is null)
        {
            return;
        }
        
        appointment.Status = AppointmentStatus.Pending.ToString();

        appointments.Update(appointment);
        
        await _dbContext.SaveChangesAsync(token);
    }
}