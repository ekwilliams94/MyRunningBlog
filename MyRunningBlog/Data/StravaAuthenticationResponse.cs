using Newtonsoft.Json;

namespace MyRunningBlog.Data
{
    public partial class RunningRepository
    {
        public class StravaAuthenticationResponse : IStravaAuthenticationResponse
        {
            [JsonProperty("token_type")]
            public string TokenType { get; set; }
            [JsonProperty("access_token")]
            public string AccessToken { get; set; }
            [JsonProperty("refresh_token")]
            public string RefreshToken { get; set; }
        }
    }
}
