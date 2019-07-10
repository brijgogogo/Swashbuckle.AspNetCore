using System.Collections.Generic;

namespace OpenApi.Generator.Mvc.Tests
{
    public class CompositeType
    {
        public ComplexType Property1 { get; set; }

        public IEnumerable<ComplexType> Property2 { get; set; }
    }
}