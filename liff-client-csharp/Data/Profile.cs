using System.Text.Json.Serialization;

namespace LineDC.Liff.Data
{
    public class Profile
    {
        [JsonPropertyName("type")]
        public string UserId { get; set; }

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("pictureUrl")]
        public string PictureUrl { get; set; }

        [JsonPropertyName("statusMessage")]
        public string StatusMessage { get; set; }

    }
}
