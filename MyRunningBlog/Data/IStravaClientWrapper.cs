using RestSharp.Portable;
using StravaSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyRunningBlog.Data
{
    public interface IStravaClientWrapper
    {
        Task<List<ActivitySummary>> GetAthleteActivities(IAuthenticator authenticator);
    }
}
