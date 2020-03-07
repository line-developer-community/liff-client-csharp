using System.Text.Json.Serialization;

namespace LineDC.Liff.Data
{
    public class IdTokenPayload
    {
        [JsonPropertyName("iss")]
        public string Iss { get; set; }
        [JsonPropertyName("sub")]
        public string Sub { get; set; }
        [JsonPropertyName("aud")]
        public string Aud { get; set; }
        [JsonPropertyName("exp")]
        public long Exp { get; set; }
        [JsonPropertyName("iat")]
        public long Iat { get; set; }
        [JsonPropertyName("auth_time")]
        public long AuthTime { get; set; }
        [JsonPropertyName("nonce")]
        public string Nonce { get; set; }
        [JsonPropertyName("amr")]
        public string[] Amr { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("picture")]
        public string Picture { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
    }
}
