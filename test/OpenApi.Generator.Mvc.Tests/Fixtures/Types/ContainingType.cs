namespace OpenApi.Generator.Mvc.Tests
{
    public class ContainingType
    {
        public NestedType Property1 { get; set; }

        public class NestedType
        {
            public string Property1 { get; set; }
        }
    }
}