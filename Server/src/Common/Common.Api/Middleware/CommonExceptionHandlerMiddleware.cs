using System.Net;
using Common.Application.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Common.Api.Middleware;

public class CommonExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public CommonExceptionHandlerMiddleware(RequestDelegate next) =>
        _next = next;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        var result = string.Empty;
        switch (exception)
        {
            case ValidationException validationException:
                code = HttpStatusCode.BadRequest;
                result = System.Text.Json.JsonSerializer.Serialize(validationException.Errors);
                break;
            case BadOperationException badOperationException:
                code = HttpStatusCode.BadRequest;
                result = System.Text.Json.JsonSerializer.Serialize(badOperationException.Message);
                break;
            case NotFoundException notFound:
                code = HttpStatusCode.NotFound;
                result = System.Text.Json.JsonSerializer.Serialize(notFound.Message);
                break;
            case ForbiddenException forbiddenException:
                code = HttpStatusCode.Forbidden;
                result = System.Text.Json.JsonSerializer.Serialize(forbiddenException.Message);
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        if (result == string.Empty)
        {
            result = System.Text.Json.JsonSerializer.Serialize(new { errpr = exception.Message });
        }

        return context.Response.WriteAsync(result);
    }
}