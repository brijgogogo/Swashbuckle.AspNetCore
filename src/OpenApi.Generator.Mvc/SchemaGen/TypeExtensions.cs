using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenApi.Generator.Mvc
{
    public static class TypeExtensions
    {
        public static bool IsNullable(this Type type)
        {
            return type.FullName.StartsWith("System.Nullable`1");
        }

        public static bool IsFSharpOption(this Type type)
        {
            return type.FullName.StartsWith("Microsoft.FSharp.Core.FSharpOption`1");
        }

        public static bool IsSet(this Type type)
        {
            return new[] { type }
                .Union(type.GetInterfaces())
                .Any(i => i.IsConstructedGenericType && i.GetGenericTypeDefinition() == typeof(ISet<>));
        }

        public static string GetFriendlyName(this Type type)
        {
            if (type == typeof(int))
                return "int";
            else if (type == typeof(short))
                return "short";
            else if (type == typeof(byte))
                return "byte";
            else if (type == typeof(bool))
                return "bool";
            else if (type == typeof(long))
                return "long";
            else if (type == typeof(float))
                return "float";
            else if (type == typeof(double))
                return "double";
            else if (type == typeof(decimal))
                return "decimal";
            else if (type == typeof(string))
                return "string";
            else if (type.IsGenericType)
                return type.Name.Split('`')[0] + "<" + string.Join(", ", type.GetGenericArguments().Select(x => GetFriendlyName(x)).ToArray()) + ">";
            else
                return type.Name;
        }

        public static string GetTypeName(Type t)
        {
            return RemoveAssemblyDetails(t.AssemblyQualifiedName);
        }

        private static string RemoveAssemblyDetails(string fullyQualifiedTypeName)
        {
            StringBuilder builder = new StringBuilder();

            // loop through the type name and filter out qualified assembly details from nested type names
            bool writingAssemblyName = false;
            bool skippingAssemblyDetails = false;
            for (int i = 0; i < fullyQualifiedTypeName.Length; i++)
            {
                char current = fullyQualifiedTypeName[i];
                switch (current)
                {
                    case '[':
                    case ']':
                        writingAssemblyName = false;
                        skippingAssemblyDetails = false;
                        builder.Append(current);
                        break;
                    case ',':
                        if (!writingAssemblyName)
                        {
                            writingAssemblyName = true;
                            builder.Append(current);
                        }
                        else
                        {
                            skippingAssemblyDetails = true;
                        }
                        break;
                    default:
                        if (!skippingAssemblyDetails)
                        {
                            builder.Append(current);
                        }
                        break;
                }
            }

            return builder.ToString();
        }
    }
}
