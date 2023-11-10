using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace VS.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddVcApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() }, includeInternalTypes: true);
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}