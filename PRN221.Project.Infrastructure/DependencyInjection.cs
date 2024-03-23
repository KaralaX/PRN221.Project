using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PRN221.Project.Application.Common.Interfaces;
using PRN221.Project.Application.Common.Services;
using PRN221.Project.Infrastructure.Identity;
using PRN221.Project.Infrastructure.Persistence;
using PRN221.Project.Infrastructure.Services;
using PRN221.Project.Infrastructure.VnPay.Services;

namespace PRN221.Project.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
        );

        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

        services.AddIdentity<ApplicationUser, IdentityRole>(
                options => { options.SignIn.RequireConfirmedAccount = false; })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.Configure<AdminSettings>(configuration.GetSection(AdminSettings.SectionName));

        services.AddScoped<ApplicationDbContextInitializer>();

        services.AddTransient<IDateTimeProvider, DateTimeProvider>();

        services.AddTransient<IEmailSender, EmailSender>();

        services.AddTransient<IVnPayService, VnPayService>();
        
        return services;
    }
}