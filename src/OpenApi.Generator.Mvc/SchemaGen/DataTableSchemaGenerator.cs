using System;
using System.Collections.Generic;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;

namespace OpenApi.Generator.Mvc
{
    public class DataTableSchemaGenerator : ChainableSchemaGenerator
    {
        public DataTableSchemaGenerator(
            IContractResolver contractResolver,
            ISchemaGenerator rootGenerator,
            SchemaGeneratorOptions options)
            : base(contractResolver, rootGenerator, options)
        { }

        protected override bool CanGenerateSchemaFor(Type type)
        {
            return ContractResolver.ResolveContract(type) is JsonISerializableContract;
        }

        protected override OpenApiSchema GenerateSchemaFor(Type type, SchemaRepository schemaRepository)
        {
            var jsonContract = (JsonISerializableContract)ContractResolver.ResolveContract(type);

            if (!schemaRepository.TryGetIdFor(type, out string schemaId))
            {
                schemaId = Options.SchemaIdSelector(type);
                schemaRepository.ReserveIdFor(type, schemaId);

                schemaRepository.AddSchemaFor(type, CreateSchema(schemaRepository));
            }

            return new OpenApiSchema
            {
                Reference = new OpenApiReference { Id = schemaId, Type = ReferenceType.Schema }
            };
        }

        private OpenApiSchema CreateSchema(SchemaRepository schemaRepository)
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
    }
}