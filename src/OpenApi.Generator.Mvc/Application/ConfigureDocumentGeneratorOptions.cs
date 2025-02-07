using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace OpenApi.Generator.Mvc
{
    internal class ConfigureDocumentGeneratorOptions : IConfigureOptions<DocumentGeneratorOptions>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly DocumentGenOptions _swaggerGenOptions;

        public ConfigureDocumentGeneratorOptions(
            IServiceProvider serviceProvider,
            IOptions<DocumentGenOptions> swaggerGenOptionsAccessor)
        {
            _serviceProvider = serviceProvider;
            _swaggerGenOptions = swaggerGenOptionsAccessor.Value;
        }

        public void Configure(DocumentGeneratorOptions options)
        {
            DeepCopy(_swaggerGenOptions.SwaggerGeneratorOptions, options);

            // Create and add any filters that were specified through the FilterDescriptor lists ...

            _swaggerGenOptions.ParameterFilterDescriptors.ForEach(
                filterDescriptor => options.ParameterFilters.Add(CreateFilter<IParameterFilter>(filterDescriptor)));

            _swaggerGenOptions.OperationFilterDescriptors.ForEach(
                filterDescriptor => options.OperationFilters.Add(CreateFilter<IOperationFilter>(filterDescriptor)));

            _swaggerGenOptions.DocumentFilterDescriptors.ForEach(
                filterDescriptor => options.DocumentFilters.Add(CreateFilter<IDocumentFilter>(filterDescriptor)));
        }

        public void DeepCopy(DocumentGeneratorOptions source, DocumentGeneratorOptions target)
        {
            target.SwaggerDocs = new Dictionary<string, OpenApiInfo>(source.SwaggerDocs);
            target.DocInclusionPredicate = source.DocInclusionPredicate;
            target.IgnoreObsoleteActions = source.IgnoreObsoleteActions;
            target.ConflictingActionsResolver = source.ConflictingActionsResolver;
            target.OperationIdSelector = source.OperationIdSelector;
            target.TagsSelector = source.TagsSelector;
            target.SortKeySelector = source.SortKeySelector;
            target.DescribeAllParametersInCamelCase = source.DescribeAllParametersInCamelCase;
            target.SecuritySchemes = new Dictionary<string, OpenApiSecurityScheme>(source.SecuritySchemes);
            target.SecurityRequirements = new List<OpenApiSecurityRequirement>(source.SecurityRequirements);
            target.ParameterFilters = new List<IParameterFilter>(source.ParameterFilters);
            target.OperationFilters = new List<IOperationFilter>(source.OperationFilters);
            target.DocumentFilters = new List<IDocumentFilter>(source.DocumentFilters);
        }

        private TFilter CreateFilter<TFilter>(FilterDescriptor filterDescriptor)
        {
            return (TFilter)ActivatorUtilities
                .CreateInstance(_serviceProvider, filterDescriptor.Type, filterDescriptor.Arguments);
        }
    }
}