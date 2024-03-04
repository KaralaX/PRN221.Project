using PRN221.Project.Application.Common.Interfaces;
using PRN221.Project.Domain.Entities;

namespace PRN221.Project.Application.Appointments.Queries;

public record ListAppointmentQuery(
    Guid scheduleId,
    Guid patiendID) : IRequest<IEnumerable<Appointment>>;

public class ListAppointmentQueryHandler : IRequestHandler<ListAppointmentQuery, IEnumerable<Appointment>>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public ListAppointmentQueryHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<IEnumerable<Appointment>> Handle(ListAppointmentQuery request,
        CancellationToken cancellationToken)
    {
        var appointments = _applicationDbContext.Appointments;

        return appointments.Where(x =>
            x.ScheduleId == request.scheduleId
            && x.PatientId == request.patiendID
        ).ToList();
    }
}