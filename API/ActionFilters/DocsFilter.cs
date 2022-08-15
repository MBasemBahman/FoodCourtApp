using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace API.ActionFilters
{
    public class DocsFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters ??= new List<OpenApiParameter>();

            ControllerActionDescriptor ActionDescriptor = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;

            if (ActionDescriptor != null)
            {
                bool allowAll = ActionDescriptor.EndpointMetadata.OfType<AllowAllAttribute>().Any();
                bool allowAnonymous = ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();

                if (!allowAll)
                {
                    operation.Parameters.Add(new OpenApiParameter
                    {
                        Name = HeadersConstants.Validator,
                        In = ParameterLocation.Header,
                        Required = true,
                        Schema = new OpenApiSchema
                        {
                            Type = "string"
                        }
                    });
                }
                if (!allowAnonymous && !allowAll)
                {
                    operation.Parameters.Add(new OpenApiParameter
                    {
                        Name = HeadersConstants.AuthorizationToken,
                        In = ParameterLocation.Header,
                        Required = true,
                        Schema = new OpenApiSchema
                        {
                            Type = "string"
                        }
                    });
                }
            }
        }
    }
}
