using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ApiDescriptions;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DocumentGenServiceCollectionExtensions
    {
        private static Type DocumentationProvider = typeof(DocumentGenerator);

        public static void RegisterDocumentationProvider<T>() where T : IDocumentationProvider
        {
            DocumentationProvider = typeof(T);
        }

        public static IServiceCollection AddSwaggerGen(
            this IServiceCollection services,
            Action<DocumentGenOptions> setupAction = null)
        {
            // Add Mvc convention to ensure ApiExplorer is enabled for all actions
            services.Configure<MvcOptions>(c =>
                c.Conventions.Add(new DocumentApplicationConvention()));

            // Register generator and it's dependencies
#if NETCOREAPP3_0
            services.AddTransient<ISerializerSettingsAccessor, MvcNewtonsoftJsonOptionsAccessor>();
#else
            services.AddTransient<ISerializerSettingsAccessor, MvcJsonOptionsAccessor>();
#endif

            services.AddTransient(typeof(IDocumentationProvider), DocumentationProvider);

            services.AddTransient<ISchemaGenerator, SchemaGenerator>();

            // Register custom configurators that assign values from SwaggerGenOptions (i.e. high level config)
            // to the service-specific options (i.e. lower-level config)
            services.AddTransient<IConfigureOptions<DocumentGeneratorOptions>, ConfigureDocumentGeneratorOptions>();
            services.AddTransient<IConfigureOptions<SchemaGeneratorOptions>, ConfigureSchemaGeneratorOptions>();

            // Used by the <c>dotnet-getdocument</c> tool from the Microsoft.Extensions.ApiDescription.Server package.
            services.TryAddSingleton<IDocumentProvider, DocumentProvider>();

            if (setupAction != null) services.ConfigureSwaggerGen(setupAction);

            return services;
        }

        public static void ConfigureSwaggerGen(
            this IServiceCollection services,
            Action<DocumentGenOptions> setupAction)
        {
            services.Configure(setupAction);
        }

#if NETCOREAPP3_0
        private sealed class MvcNewtonsoftJsonOptionsAccessor : ISerializerSettingsAccessor
        {
            private readonly IOptions<MvcNewtonsoftJsonOptions> _options;

            public MvcNewtonsoftJsonOptionsAccessor(IOptions<MvcNewtonsoftJsonOptions> options)
            {
                _options = options;
            }

            public JsonSerializerSettings Value => _options.Value?.SerializerSettings;
        }
#else
        private sealed class MvcJsonOptionsAccessor : ISerializerSettingsAccessor
        {
            private readonly IOptions<MvcJsonOptions> _options;

            public MvcJsonOptionsAccessor(IOptions<MvcJsonOptions> options)
            {
                _options = options;
            }

            public JsonSerializerSettings Value => _options.Value?.SerializerSettings;
        }
#endif
    }
}
