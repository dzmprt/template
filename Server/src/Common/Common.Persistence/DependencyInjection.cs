using Common.Application.Abstractions.Persistence;
using Common.Application.Abstractions.Persistence.Repository.Read;
using Common.Application.Abstractions.Persistence.Repository.Writing;
using Common.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddCommonPersistenceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());
        services.AddTransient(typeof(IBaseReadRepository<>), typeof(BaseRepository<>));
        services.AddTransient(typeof(IBaseWriteRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IDatabaseMigrator, DatabaseMigrator>();

        return services;
    }
}