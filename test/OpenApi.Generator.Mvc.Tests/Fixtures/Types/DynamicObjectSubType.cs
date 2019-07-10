using System.Dynamic;
using Newtonsoft.Json;

namespace OpenApi.Generator.Mvc.Tests
{
    [JsonObject]
    public class DynamicObjectSubType : DynamicObject
    {
        public string Property1 { get; set; }
    }
}