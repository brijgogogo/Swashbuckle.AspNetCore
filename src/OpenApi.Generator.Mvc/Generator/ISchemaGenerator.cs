using System;
using Microsoft.OpenApi.Models;

namespace OpenApi.Generator.Mvc
{
    public interface ISchemaGenerator
    {
        OpenApiSchema GenerateSchema(Type type, SchemaRepository schemaRepository);
    }
}
