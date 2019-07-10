using System.Collections.Generic;

using Newtonsoft.Json;

namespace OpenApi.Generator.Mvc.Tests
{
    public class ExtensionDataAnnotatedType
    {
        public bool Property1 { get; set; }

        [JsonExtensionData]
        public IDictionary<string, object> ExtensionData { get; set; }
    }
}