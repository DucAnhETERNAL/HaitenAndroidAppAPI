using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public class SwaggerFileOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.RequestBody == null)
        {
            return;
        }

        var fileParams = context.MethodInfo.GetParameters()
            .Where(p => p.ParameterType == typeof(IFormFile));

        if (!fileParams.Any())
        {
            return;
        }

        operation.RequestBody.Content["multipart/form-data"] = new OpenApiMediaType
        {
            Schema = new OpenApiSchema
            {
                Type = "object",
                Properties = fileParams.ToDictionary(
                    param => param.Name,
                    param => new OpenApiSchema
                    {
                        Type = "string",
                        Format = "binary"
                    })
            }
        };
    }
}
