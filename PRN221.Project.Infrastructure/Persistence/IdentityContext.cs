using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PRN221.Project.Domain.Enums;

namespace PRN221.Project.Infrastructure.Persistence;

public class IdentityContext : IdentityDbContext
{
    public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        //Seed roles
        var roles = Enum.GetValues<UserType>().Select((type, id) => new IdentityRole
        {
            Id = id.ToString(),
            Name = type.ToString(),
            NormalizedName = type.ToString().ToUpper()
        });

        builder.Entity<IdentityRole>().HasData(
            roles
        );
    }
}