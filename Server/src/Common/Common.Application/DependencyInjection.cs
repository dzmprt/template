using Common.Application.Abstractions.Service;
using Common.Application.Behavior;
using Common.Application.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddCommonApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IPermissionValidatorService, PermissionValidatorService>();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(DatabaseTransactionBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

        return services;
    }
}