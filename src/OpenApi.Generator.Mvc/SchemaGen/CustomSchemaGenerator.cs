using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;

namespace OpenApi.Generator.Mvc
{
    public class CustomSchemaGenerator : ChainableSchemaGenerator
    {
        public CustomSchemaGenerator(
            IContractResolver contractResolver,
            ISchemaGenerator rootGenerator,
            SchemaGeneratorOptions options)
            : base(contractResolver, rootGenerator, options)
        { }

        protected override bool CanGenerateSchemaFor(Type type)
        {
            return type == typeof(DataTable) || type == typeof(Exception);
        }

        protected override OpenApiSchema GenerateSchemaFor(Type type, SchemaRepository schemaRepository)
        {
            if (!schemaRepository.TryGetIdFor(type, out string schemaId))
            {
                schemaId = Options.SchemaIdSelector(type);
                schemaRepository.ReserveIdFor(type, schemaId);

                if (type == typeof(DataTable))
                {
                    schemaRepository.AddSchemaFor(type, CreateDataTableSchema(schemaRepository));
                }
                else if (type == typeof(Exception))
                {
                    schemaRepository.AddSchemaFor(type, CreateExceptionSchema(schemaRepository));
                }
            }

            return new OpenApiSchema
            {
                Reference = new OpenApiReference { Id = schemaId, Type = ReferenceType.Schema }
            };
        }

        private OpenApiSchema CreateDataTableSchema(SchemaRepository schemaRepository)
        {
            var schema = new OpenApiSchema
            {
                Type = "array",
                Items = new OpenApiSchema
                {
                    Properties = new Dictionary<string, OpenApiSchema>
                    {
                        ["Column1"] = RootGenerator.GenerateSchema(typeof(int), schemaRepository),
                        ["Column2"] = RootGenerator.GenerateSchema(typeof(double), schemaRepository),
                        ["Column3"] = RootGenerator.GenerateSchema(typeof(short), schemaRepository),
                        ["Column4"] = RootGenerator.GenerateSchema(typeof(long), schemaRepository),
                        ["Column5"] = RootGenerator.GenerateSchema(typeof(string), schemaRepository),
                    }
                }
            };

            return schema;
        }

        private OpenApiSchema CreateExceptionSchema(SchemaRepository schemaRepository)
        {
            return new OpenApiSchema
            {
                Type = "object",
                Properties = new Dictionary<string, OpenApiSchema>
                {
                    ["Message"] = RootGenerator.GenerateSchema(typeof(string), schemaRepository),
                    ["StackTraceString"] = RootGenerator.GenerateSchema(typeof(string), schemaRepository),
                    ["Source"] = RootGenerator.GenerateSchema(typeof(string), schemaRepository),
                }
            };
        }
    }
}