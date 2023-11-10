using System.Text.Json.Serialization;
using Common.Api.JsonSerializer;
using Common.Api.Services;
using Common.Application.Abstractions.Service;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddCommonApiServices(this IServiceCollection services,  IConfiguration configuration)
    {
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddHttpContextAccessor();
        
        services.Configure<JsonOptions>(options =>
        {
            options.SerializerOptions.Converters.Add(new TrimmingConverter());
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
            options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        });
        
        return services;
    }
}