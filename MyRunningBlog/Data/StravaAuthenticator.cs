using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using static MyRunningBlog.Data.RunningRepository;
using Microsoft.Extensions.Configuration;
using MyRunningBlog.Models.Configuration;
using RestSharp.Portable;

namespace MyRunningBlog.Data
{
    public class StravaAuthenticator: IStravaAuthenticator
    {
        private static HttpClient _httpClient = new HttpClient();
        private IConfiguration _configuration { get; }

        public StravaAuthenticator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IAuthenticator> CreateAuthenticatorAsync()
        {
            var stravaSettings = _configuration.GetSection("Strava").Get<StravaSettings>();
            var values = new Dictionary<string, string>
                {
                       { "client_id", stravaSettings.ClientId },
                       { "client_secret", stravaSettings.ClientSecret },
                       { "refresh_token", stravaSettings.RefeshToken },
                       { "grant_type", stravaSettings.GrantType }

                };

            var content = new FormUrlEncodedContent(values);
            var response = await _httpClient.PostAsync("https://www.strava.com/oauth/token", content);
            var responseString = await response.Content.ReadAsStringAsync();
            var stravaAuthenticationResponse = JsonConvert.DeserializeObject<StravaAuthenticationResponse>(responseString);


            return new Authenticator(stravaAuthenticationResponse.AccessToken);
        }
    }
}
