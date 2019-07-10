using Newtonsoft.Json;

namespace OpenApi.Generator.Mvc
{
    public interface ISerializerSettingsAccessor
    {
        JsonSerializerSettings Value { get; }
    }
}
