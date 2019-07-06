using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace LineDC.Liff.Data
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class LiffContext
    {
        public ContextType Type { get; set; }
        public ViewType ViewType { get; set; }
        public string UserId { get; set; }
        public string UtouId { get; set; }
        public string RoomId { get; set; }
        public string GroupId { get; set; }
        public LiffContext(){}
    }
}
