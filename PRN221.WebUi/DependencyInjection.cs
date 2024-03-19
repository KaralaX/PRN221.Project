using PRN221.Project.Application.Common.Interfaces;
using PRN221.WebUi.Services;

namespace PRN221.WebUi;

public static class DependencyInjection
{
    public static IServiceCollection AddWebUiServices(this IServiceCollection services)
    {
        services.AddRazorPages();

        services.AddScoped<IUser, CurrentUser>();

        services.ConfigureApplicationCookie(options => options.AccessDeniedPath = "/Identity/Account/AccessDenied");

        return services;
    }
}