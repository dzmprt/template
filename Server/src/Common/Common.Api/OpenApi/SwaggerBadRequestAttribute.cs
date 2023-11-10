using Swashbuckle.AspNetCore.Annotations;

namespace Common.Api.OpenApi;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class SwaggerBadRequestAttribute : SwaggerResponseAttribute
{
    private static readonly HashSet<string> _errorCodes = new();

    public static string[] ErrorCodes => _errorCodes.ToArray();

    public SwaggerBadRequestAttribute()
        : base(400, "Bad request", typeof(Microsoft.AspNetCore.Mvc.ProblemDetails))
    {
    }

    public SwaggerBadRequestAttribute(params Type[] badOperations)
        : base(
            400,
            GetFormattedDescription(badOperations),
            typeof(Microsoft.AspNetCore.Mvc.ProblemDetails)
        )
    {
    }

    private static string GetFormattedDescription(Type[] badOperations)
    {
        var errors = badOperations
            .Select(t => t.Name)
            .Select(error => error)
            .ToArray();

        foreach (var error in errors)
        {
            _errorCodes.Add(error);
        }

        var formattedErrors = string.Join(", ", errors.Select(error => $"'{error}'"));
        return $"Bad request with errors: {formattedErrors}";
    }
}