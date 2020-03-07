using System.Text.Json.Serialization;

namespace LineDC.Liff.Data
{
    public class GetFrendship
    {
        [JsonPropertyName("friendFlag")]
        public bool FriendFlag { get; set; }
    }
}
