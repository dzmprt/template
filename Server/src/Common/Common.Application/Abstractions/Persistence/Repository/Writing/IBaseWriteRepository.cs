using Common.Application.Abstractions.Persistence.Repository.Read;

namespace Common.Application.Abstractions.Persistence.Repository.Writing;

public interface IBaseWriteRepository<TEntity> : IBaseReadRepository<TEntity> where TEntity : class
{
    Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken);
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);
    Task RemoveAsync(TEntity entity, CancellationToken cancellationToken);
    Task RemoveRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);
}