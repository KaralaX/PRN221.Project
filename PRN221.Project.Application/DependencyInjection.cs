using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PRN221.Project.Application.Common.Behaviours;

namespace PRN221.Project.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            config.AddOpenBehavior(typeof(ValidationBehaviour<,>), ServiceLifetime.Scoped);
        });

        return services;
    }
}