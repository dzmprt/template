namespace Common.Application.Abstractions.Persistence.Repository.Read;

public interface IBaseReadRepository<out TEntity> : IAsyncEnumerable<TEntity> where TEntity: class
{
}