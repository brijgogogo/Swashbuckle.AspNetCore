using System;
using System.Linq;

namespace OpenApi.Generator.Mvc
{
    public class SchemaIdSelectors
    {
        public static string FriendlyNameSelector(Type modelType)
        {
            return $"{modelType.Namespace}.{modelType.GetFriendlyName()}";
        }

        public static string Default(Type modelType)
        {
            if (!modelType.IsConstructedGenericType) return modelType.Name;

            var prefix = modelType.GetGenericArguments()
                .Select(genericArg => Default(genericArg))
                .Aggregate((previous, current) => previous + current);

            return prefix + modelType.Name.Split('`').First();
        }
    }
}