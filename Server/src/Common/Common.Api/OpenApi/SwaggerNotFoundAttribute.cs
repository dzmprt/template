using Swashbuckle.AspNetCore.Annotations;

namespace Common.Api.OpenApi;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class SwaggerNotFoundAttribute : SwaggerResponseAttribute
{
    public SwaggerNotFoundAttribute(string description = "Resource was not found")
        : base(404, description, typeof(Microsoft.AspNetCore.Mvc.ProblemDetails))
    {
    }
}