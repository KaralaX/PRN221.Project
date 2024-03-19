using Microsoft.EntityFrameworkCore;
using PRN221.Project.Application.Common.Interfaces;
using PRN221.Project.Domain.Entities;

namespace PRN221.Project.Application.Doctors.Queries.ListDoctor;

public sealed record ListDoctorQuery : IRequest<IQueryable<Doctor>>;

public sealed class ListDoctorQueryHandler : IRequestHandler<ListDoctorQuery, IQueryable<Doctor>>
{
    private readonly IApplicationDbContext _dbContext;

    public ListDoctorQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IQueryable<Doctor>> Handle(ListDoctorQuery request, CancellationToken cancellationToken)
    {
        return _dbContext.Doctors.Include(x => x.PersonalInformation).AsNoTracking();
    }
}
