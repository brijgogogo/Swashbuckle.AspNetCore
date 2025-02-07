﻿using System;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace OpenApi.Generator.Mvc.Tests
{
    public class VendorExtensionsDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            context.SchemaGenerator.GenerateSchema(typeof(DateTime), context.SchemaRepository);
            swaggerDoc.Extensions.Add("X-property1", new OpenApiString("value"));
        }
    }
}