namespace Common.Application.Abstractions.Persistence;

public interface IApplicationDbContext
{
    Task<IContextTransaction> BeginTransaction(CancellationToken cancellationToken);
}