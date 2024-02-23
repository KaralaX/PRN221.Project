namespace PRN221.Project.WebUI;

public static class DependencyInjection
{
    public static IServiceCollection AddWebUiServices(this IServiceCollection services)
    {
        services.AddDatabaseDeveloperPageExceptionFilter();
        services.AddRazorPages();

        return services;
    }
}