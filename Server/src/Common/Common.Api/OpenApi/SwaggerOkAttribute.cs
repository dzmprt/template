using Swashbuckle.AspNetCore.Annotations;

namespace Common.Api.OpenApi;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class SwaggerOkAttribute : SwaggerResponseAttribute
{
    public SwaggerOkAttribute(string description = null)
        : base(200, description)
    {
    }

    public SwaggerOkAttribute(
        Type type,
        string description = null
    )
        : base(200, description, type)
    {
        Description = description;
    }
}