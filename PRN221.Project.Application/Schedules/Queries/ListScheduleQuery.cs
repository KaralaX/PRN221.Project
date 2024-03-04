
using PRN221.Project.Application.Common.Interfaces;
using PRN221.Project.Domain.Entities;

namespace PRN221.Project.Application.Schedules.Queries;

public class ListScheduleQuery : IRequest<IEnumerable<Schedule>>
{
    public int PageIndex { get; set; }

    public string DoctorName { get; set; }
}


public class ListScheduleQueryHandler : IRequestHandler<ListScheduleQuery, IEnumerable<Schedule>>
{
    private readonly IApplicationDbContext _context;

    public ListScheduleQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Schedule>> Handle(ListScheduleQuery request, CancellationToken cancellationToken)
    {
        var schedules = _context.Schedules;

        List<Schedule> list = new List<Schedule>();

        return schedules.ToList();
    }
}
