using Microsoft.EntityFrameworkCore;
using PRN221.Project.Application.Common.Interfaces;
using PRN221.Project.Domain.Enums;

namespace PRN221.Project.Application.Appointments.Commands;

public record EndAppointmentCommand(Guid AppointmentId) : IRequest;

public class EndAppointmentCommandHandler : IRequestHandler<EndAppointmentCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public EndAppointmentCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(EndAppointmentCommand request, CancellationToken token)
    {
        var appointments = _dbContext.Appointments;

        var appointment = await appointments.FirstOrDefaultAsync(a => a.Id == request.AppointmentId, cancellationToken: token);

        if (appointment is null)
        {
            return;
        }   
        
        appointment.Status = AppointmentStatus.Finished.ToString();

        appointments.Update(appointment);
        
        await _dbContext.SaveChangesAsync(token);
    }
}