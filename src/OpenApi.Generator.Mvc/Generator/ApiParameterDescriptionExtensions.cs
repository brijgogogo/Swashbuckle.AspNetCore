﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace OpenApi.Generator.Mvc
{
    public static class ApiParameterDescriptionExtensions
    {
        public static ParameterInfo ParameterInfo(this ApiParameterDescription apiParameter)
        {
            var controllerParameterDescriptor = apiParameter.ParameterDescriptor as ControllerParameterDescriptor;
            return controllerParameterDescriptor?.ParameterInfo;
        }

        public static PropertyInfo PropertyInfo(this ApiParameterDescription apiParameter)
        {
            var modelMetadata = apiParameter.ModelMetadata;

            return (modelMetadata?.ContainerType != null)
                ? modelMetadata.ContainerType.GetProperty(modelMetadata.PropertyName)
                : null;
        }

        public static IEnumerable<object> CustomAttributes(this ApiParameterDescription apiParameter)
        {
            var parameterInfo = apiParameter.ParameterInfo();
            var parameterAttributes = (parameterInfo != null) ? parameterInfo.GetCustomAttributes(true) : Enumerable.Empty<object>();

            var propertyInfo = apiParameter.PropertyInfo();
            var propertyAttributes = (propertyInfo != null) ? propertyInfo.GetCustomAttributes(true) : Enumerable.Empty<object>();

            return parameterAttributes.Union(propertyAttributes);
        }

        [Obsolete("Use ParameterInfo(), PropertyInfo() and CustomAttributes() extension methods instead")]
        internal static void GetAdditionalMetadata(
            this ApiParameterDescription apiParameter,
            ApiDescription apiDescription,
            out ParameterInfo parameterInfo,
            out PropertyInfo propertyInfo,
            out IEnumerable<object> parameterOrPropertyAttributes)
        {
            parameterInfo = apiParameter.ParameterInfo();
            propertyInfo = apiParameter.PropertyInfo();
            parameterOrPropertyAttributes = apiParameter.CustomAttributes();
        }

        internal static bool IsFromPath(this ApiParameterDescription apiParameter)
        {
            return (apiParameter.Source == BindingSource.Path);
        }

        internal static bool IsFromBody(this ApiParameterDescription apiParameter)
        {
            return (apiParameter.Source == BindingSource.Body);
        }

        internal static bool IsFromForm(this ApiParameterDescription apiParameter)
        {
            var source = apiParameter.Source;
            var elementType = apiParameter.ModelMetadata?.ElementType;

            return (source == BindingSource.Form || source == BindingSource.FormFile)
                || (elementType != null && typeof(IFormFile).IsAssignableFrom(elementType));
        }
    }
}