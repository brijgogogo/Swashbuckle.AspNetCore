using System;
using System.Collections.Generic;
using Microsoft.OpenApi.Models;

namespace OpenApi.Generator.Mvc.Tests
{
    public class RecursiveCallSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema model, SchemaFilterContext context)
        {
            model.Properties = new Dictionary<string, OpenApiSchema>();
            model.Properties.Add("ExtraProperty", context.SchemaGenerator.GenerateSchema(typeof(ComplexType), context.SchemaRepository));
        }
    }
}
