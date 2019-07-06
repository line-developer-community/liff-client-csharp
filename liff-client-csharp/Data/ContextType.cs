using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace LineDC.Liff.Data
{
    [JsonConverter(typeof(StringEnumConverter), new object[] { typeof(CamelCaseNamingStrategy) })]
    public enum ContextType
    {
        Utou,
        Room,
        Group,
        None
    }
}
