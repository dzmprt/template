using Microsoft.AspNetCore.Builder;

namespace Common.Api.Middleware;

public static class CommonExceptionHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseCommonExceptionHandler(this
        IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CommonExceptionHandlerMiddleware>();
    }
}