﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;

namespace OpenApi.Generator.Mvc
{
    public interface IDocumentFilter
    {
        void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context);
    }

    public class DocumentFilterContext
    {
        public DocumentFilterContext(
            IEnumerable<ApiDescription> apiDescriptions,
            ISchemaGenerator schemaGenerator,
            SchemaRepository schemaRepository)
        {
            ApiDescriptions = apiDescriptions;
            SchemaGenerator = schemaGenerator;
            SchemaRepository = schemaRepository;
        }

        public IEnumerable<ApiDescription> ApiDescriptions { get; }

        public ISchemaGenerator SchemaGenerator { get; }

        public SchemaRepository SchemaRepository { get; }
    }
}
