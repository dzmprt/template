using System.Text.Json;
using Common.Application.Abstractions.Service;
using MediatR;
using Serilog;

namespace Common.Application.Behavior;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : IRequest<TResponse>
{
    private readonly ICurrentUserService _currentUserService;

    public LoggingBehavior(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var userId = _currentUserService.UserId;

        Log.Information("Request: {Name} {@UserId} {@Request}",
            requestName, userId, request);

        var response = await next();
        Log.Information($"Response: { JsonSerializer.Serialize(response)}");

        return response;
    }
}