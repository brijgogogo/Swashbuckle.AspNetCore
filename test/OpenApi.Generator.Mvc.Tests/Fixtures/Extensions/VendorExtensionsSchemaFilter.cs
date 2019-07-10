using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace OpenApi.Generator.Mvc.Tests
{
    public class VendorExtensionsSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            schema.Extensions.Add("X-property1", new OpenApiString("value"));
        }
    }
}