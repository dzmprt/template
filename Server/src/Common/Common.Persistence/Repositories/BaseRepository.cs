using Common.Application.Abstractions.Persistence.Repository.Writing;
using Microsoft.EntityFrameworkCore;

namespace Common.Persistence.Repositories;

internal class BaseRepository<TEntity> : IBaseWriteRepository<TEntity> where TEntity : class
{
    protected readonly DbContext DbContext;

    protected readonly DbSet<TEntity> DbSet;
    
    public BaseRepository(ApplicationDbContext dbContext)
    {
        DbContext = dbContext;
        DbSet = DbContext.Set<TEntity>();
    }

    public IAsyncEnumerator<TEntity> GetAsyncEnumerator(CancellationToken cancellationToken = new CancellationToken())
    {
        return DbSet.GetAsyncEnumerator(cancellationToken);
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
    {
        DbSet.AddRange(entities);
        await DbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        DbSet.Add(entity);
        await DbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        DbSet.Update(entity);
        await DbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
    {
        DbSet.UpdateRange(entities);
        await DbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveAsync(TEntity entity, CancellationToken cancellationToken)
    {
        DbSet.Remove(entity);
        await DbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
    {
        DbSet.RemoveRange(entities);
        await DbContext.SaveChangesAsync(cancellationToken);
    }
}