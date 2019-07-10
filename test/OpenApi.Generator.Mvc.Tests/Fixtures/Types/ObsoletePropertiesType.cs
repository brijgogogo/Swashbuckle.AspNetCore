using System;

namespace OpenApi.Generator.Mvc.Tests
{
    public class ObsoletePropertiesType
    {
        public string Property1 { get; set; }

        [Obsolete]
        public string ObsoleteProperty { get; set; }
    }
}