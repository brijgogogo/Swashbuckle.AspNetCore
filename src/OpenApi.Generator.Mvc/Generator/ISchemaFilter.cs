using System;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;

namespace OpenApi.Generator.Mvc
{
    public interface ISchemaFilter
    {
        void Apply(OpenApiSchema schema, SchemaFilterContext context);
    }

    public class SchemaFilterContext
    {
        public SchemaFilterContext(
            Type type,
            JsonContract jsonContract,
            SchemaRepository schemaRepository,
            ISchemaGenerator schemaGenerator)
        {
            Type = type;
            JsonContract = jsonContract;
            SchemaRepository = schemaRepository;
            SchemaGenerator = schemaGenerator;
        }

        public Type Type { get; }

        public JsonContract JsonContract { get; }

        public SchemaRepository SchemaRepository { get; }

        public ISchemaGenerator SchemaGenerator { get; }
    }
}