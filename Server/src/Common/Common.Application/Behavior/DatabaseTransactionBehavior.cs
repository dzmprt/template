using Common.Application.Abstractions.Persistence;
using MediatR;

namespace Common.Application.Behavior;

public class DatabaseTransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : IRequest<TResponse>
{
    private readonly IApplicationDbContext _applicationContext;

    public DatabaseTransactionBehavior(IApplicationDbContext applicationContext)
    {
        _applicationContext = applicationContext;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        using var transaction = await _applicationContext.BeginTransaction(cancellationToken);
        try
        {
            var result = await next();
            await transaction.CommitAsync(cancellationToken);
            return result;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(CancellationToken.None);
            throw;
        }
    }
}