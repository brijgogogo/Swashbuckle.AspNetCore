using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;

namespace OpenApi.Generator.Mvc
{
    public class FileSchemaGenerator : ChainableSchemaGenerator
    {
        public FileSchemaGenerator(
            IContractResolver contractResolver,
            ISchemaGenerator rootGenerator,
            SchemaGeneratorOptions options)
            : base(contractResolver, rootGenerator, options)
        { }

        protected override bool CanGenerateSchemaFor(Type type)
        {
            return typeof(IFormFile).IsAssignableFrom(type) || typeof(FileResult).IsAssignableFrom(type);
        }

        protected override OpenApiSchema GenerateSchemaFor(Type type, SchemaRepository schemaRepository)
        {
            return new OpenApiSchema
            {
                Type = "string",
                Format = "binary"
            };
        }
    }
}