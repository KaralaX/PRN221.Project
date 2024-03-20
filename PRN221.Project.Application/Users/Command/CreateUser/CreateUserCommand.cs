using PRN221.Project.Application.Common.Interfaces;
using PRN221.Project.Domain.Constant;
using PRN221.Project.Domain.Entities;

namespace PRN221.Project.Application.Users.Command.CreateUser;

public record CreateUserCommand(string AccountId, string Role) : IRequest;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public CreateUserCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        switch (request.Role)
        {
            case Roles.Doctor:
                await _dbContext.Doctors.AddAsync(new Doctor
                {
                    UserId = request.AccountId
                }, cancellationToken);
                
                await _dbContext.SaveChangesAsync(cancellationToken);

                break;
            case Roles.Staff:
                await _dbContext.Staffs.AddAsync(new Staff()
                {
                    UserId = request.AccountId
                }, cancellationToken);

                await _dbContext.SaveChangesAsync(cancellationToken);

                break;
            case Roles.Patient:
                await _dbContext.Patients.AddAsync(new Patient
                {
                    UserId = request.AccountId
                }, cancellationToken);
                
                await _dbContext.SaveChangesAsync(cancellationToken);

                break;
        }

    }
}