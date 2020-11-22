using RestSharp.Portable;
using StravaSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyRunningBlog.Data
{
    public class StravaClientWrapper : IStravaClientWrapper
    {
        public StravaClientWrapper()
        {

        }

        public async Task<List<ActivitySummary>> GetAthleteActivities(IAuthenticator authenticator)
        {
            var client = new Client(authenticator);
            return await client.Activities.GetAthleteActivities();
        }
    }
}
