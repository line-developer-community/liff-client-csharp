using System.Text.Json.Serialization;

namespace LineDC.Liff.Data
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ViewType
    {
        compact,
        tall,
        full
    }
}
