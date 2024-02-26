using PRN221.Project.Application.Common.Services;
using PRN221.Project.WebUI.Services;

namespace PRN221.Project.WebUI;

public static class DependencyInjection
{
    public static IServiceCollection AddWebUiServices(this IServiceCollection services)
    {
        services.AddDatabaseDeveloperPageExceptionFilter();

        //services.AddScoped<ICurrentUserService, CurrentUserService>();
        
        services.AddRazorPages();

        return services;
    }
}