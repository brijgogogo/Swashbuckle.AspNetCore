using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace OpenApi.Generator.Mvc.Tests
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum JsonConvertedEnum
    {
        Value1 = 2,
        Value2 = 4,
        X = 8
    }
}