using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Writers;
using OpenApi.Generator.Core;
using OpenApi.Generator.Mvc;

namespace Microsoft.Extensions.ApiDescriptions
{
    /// <summary>
    /// This service will be looked up by name from the service collection when using
    /// the <c>dotnet-getdocument</c> tool from the Microsoft.Extensions.ApiDescription.Server package.
    /// </summary>
    internal interface IDocumentProvider
    {
        IEnumerable<string> GetDocumentNames();

        Task GenerateAsync(string documentName, TextWriter writer);
    }

    internal class DocumentProvider : IDocumentProvider
    {
        private readonly DocumentGeneratorOptions _generatorOptions;
        private readonly DocumentationOptions _options;
        private readonly IDocumentationProvider _swaggerProvider;

        public DocumentProvider(
            IOptions<DocumentGeneratorOptions> generatorOptions,
            IOptions<DocumentationOptions> options,
            IDocumentationProvider swaggerProvider)
        {
            _generatorOptions = generatorOptions.Value;
            _options = options.Value;
            _swaggerProvider = swaggerProvider;
        }

        public IEnumerable<string> GetDocumentNames()
        {
            return _generatorOptions.SwaggerDocs.Keys;
        }

        public Task GenerateAsync(string documentName, TextWriter writer)
        {
            // Let UnknownSwaggerDocument or other exception bubble up to caller.
            var swagger = _swaggerProvider.GetSwagger(documentName, host: null, basePath: null);
            var jsonWriter = new OpenApiJsonWriter(writer);
            if (_options.SerializeAsV2)
            {
                swagger.SerializeAsV2(jsonWriter);
            }
            else
            {
                swagger.SerializeAsV3(jsonWriter);
            }

            return Task.CompletedTask;
        }
    }
}
