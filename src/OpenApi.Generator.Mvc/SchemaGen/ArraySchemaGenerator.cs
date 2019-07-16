using System;
using System.Collections.Generic;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace OpenApi.Generator.Mvc
{
    public class ArraySchemaGenerator : ChainableSchemaGenerator
    {
        private readonly JsonSerializerSettings _serializerSettings;

        public ArraySchemaGenerator(
            IContractResolver contractResolver,
            ISchemaGenerator rootGenerator,
            JsonSerializerSettings serializerSettings,
            SchemaGeneratorOptions options)
            : base(contractResolver, rootGenerator, options)
        {
            _serializerSettings = serializerSettings;
        }

        protected override bool CanGenerateSchemaFor(Type type)
        {
            return ContractResolver.ResolveContract(type) is JsonArrayContract;
        }

        protected override OpenApiSchema GenerateSchemaFor(Type type, SchemaRepository schemaRepository)
        {
            var jsonArrayContract = (JsonArrayContract)ContractResolver.ResolveContract(type);
            //var properties = new Dictionary<string, OpenApiSchema>();
            //ObjectSchemaGenerator.AddTypeAttribute(_serializerSettings, type, properties);

            return new OpenApiSchema
            {
                Type = "array",
                Items = RootGenerator.GenerateSchema(jsonArrayContract.CollectionItemType, schemaRepository),
                UniqueItems = jsonArrayContract.UnderlyingType.IsSet() ? (bool?)true : null,
            };
        }
    }
}