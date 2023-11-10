using Common.Application.Abstractions.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Common.Persistence;

internal sealed class DatabaseMigrator : IDatabaseMigrator
{
    private readonly ApplicationDbContext _dbContext;

    public DatabaseMigrator(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task MigrateAsync(CancellationToken cancellationToken)
    {
        return _dbContext.Database.MigrateAsync(cancellationToken);
    }
}