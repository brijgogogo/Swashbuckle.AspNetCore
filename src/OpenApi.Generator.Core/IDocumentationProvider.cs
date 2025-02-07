﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OpenApi.Models;

namespace OpenApi.Generator.Core
{
    public interface IDocumentationProvider
    {
        OpenApiDocument GetSwagger(
            string documentName,
            string host = null,
            string basePath = null);
    }

    public class UnknownSwaggerDocument : InvalidOperationException
    {
        public UnknownSwaggerDocument(string documentName, IEnumerable<string> knownDocuments)
            : base(string.Format("Unknown Swagger document - \"{0}\". Known Swagger documents: {1}{2}",
                documentName,
                Environment.NewLine,
                string.Join(Environment.NewLine, knownDocuments?.Select(x => $"\"{x}\""))))
        {}
    }
}