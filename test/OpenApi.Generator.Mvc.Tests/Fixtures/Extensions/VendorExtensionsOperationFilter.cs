using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace OpenApi.Generator.Mvc.Tests
{
    public class VendorExtensionsOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext contex)
        {
            operation.Extensions.Add("X-property1", new OpenApiString("value"));
        }
    }
}