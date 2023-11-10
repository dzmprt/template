using Common.Application.Abstractions.Persistence;
using Microsoft.EntityFrameworkCore.Storage;

namespace Common.Persistence;

public class ContextTransaction : IContextTransaction
{
    private readonly IDbContextTransaction _dbContextTransaction;

    public ContextTransaction(IDbContextTransaction dbContextTransaction)
    {
        _dbContextTransaction = dbContextTransaction;
    }

    public void Dispose()
    {
        _dbContextTransaction.Dispose();
    }

    public Task CommitAsync(CancellationToken cancellationToken)
    {
        return _dbContextTransaction.CommitAsync(cancellationToken);
    }

    public Task RollbackAsync(CancellationToken cancellationToken)
    {
        return _dbContextTransaction.RollbackAsync(cancellationToken);
    }
}