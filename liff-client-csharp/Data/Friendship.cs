using System.Text.Json.Serialization;

namespace LineDC.Liff.Data
{
    public class Friendship
    {
        [JsonPropertyName("friendFlag")]
        public bool FriendFlag { get; set; }
    }
}
