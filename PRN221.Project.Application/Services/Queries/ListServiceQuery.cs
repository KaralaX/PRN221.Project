using Microsoft.EntityFrameworkCore;
using PRN221.Project.Application.Common.Interfaces;
using PRN221.Project.Domain.Entities;

namespace PRN221.Project.Application.Doctors.Queries.ListDoctor;

public sealed record ListServiceQuery : IRequest<IQueryable<Service>>;

public sealed class ListServiceQueryHandler : IRequestHandler<ListServiceQuery, IQueryable<Service>>
{
    private readonly IApplicationDbContext _dbContext;

    public ListServiceQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IQueryable<Service>> Handle(ListServiceQuery request, CancellationToken cancellationToken)
    {
        return _dbContext.Services.AsNoTracking();
    }
}
