﻿using System;
using System.Collections.Generic;

namespace OpenApi.Generator.Mvc
{
    public class DocumentGenOptions
    {
        public DocumentGeneratorOptions SwaggerGeneratorOptions { get; set; } = new DocumentGeneratorOptions();

        public SchemaGeneratorOptions SchemaGeneratorOptions { get; set; } = new SchemaGeneratorOptions();

        // NOTE: Filter instances can be added directly to the options exposed above OR they can be specified in
        // the following lists. In the latter case, they will be instantiated and added when options are injected
        // into their target services. This "deferred instantiation" allows the filters to be created from the
        // DI container, thus supporting contructor injection of services within filters.

        public List<FilterDescriptor> ParameterFilterDescriptors { get; set; } = new List<FilterDescriptor>();

        public List<FilterDescriptor> OperationFilterDescriptors { get; set; } = new List<FilterDescriptor>();

        public List<FilterDescriptor> DocumentFilterDescriptors { get; set; } = new List<FilterDescriptor>();

        public List<FilterDescriptor> SchemaFilterDescriptors { get; set; } = new List<FilterDescriptor>();
    }

    public class FilterDescriptor
    {
        public Type Type { get; set; }

        public object[] Arguments { get; set; }
    }
}