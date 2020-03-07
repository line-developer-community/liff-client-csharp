using System.Text.Json.Serialization;

namespace LineDC.Liff.Data
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ContextType
    {
        utou,
        room,
        group,
        none
    }
}
