using System.Text.Json.Serialization;

namespace LineDC.Liff.Data
{
    public class LiffContext
    {
        [JsonPropertyName("type")]
        public ContextType Type { get; set; }
        [JsonPropertyName("viewType")]
        public ViewType ViewType { get; set; }
        [JsonPropertyName("userId")]
        public string UserId { get; set; }
        [JsonPropertyName("utouId")]
        public string UtouId { get; set; }
        [JsonPropertyName("roomId")]
        public string RoomId { get; set; }
        [JsonPropertyName("groupId")]
        public string GroupId { get; set; }

    }
}
