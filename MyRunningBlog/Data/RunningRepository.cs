using MyRunningBlog.Models;
using StravaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyRunningBlog.Data
{
    public partial class RunningRepository : IRunningRepository
    {
        private IStravaAuthenticator _stravaAuthenticator { get; }
        private  IStravaClientWrapper _stravaClientWrapper { get; }

        public RunningRepository(IStravaAuthenticator stravaAuthenticator, IStravaClientWrapper stravaClientWrapper)
        {
            _stravaAuthenticator = stravaAuthenticator;
            _stravaClientWrapper = stravaClientWrapper;
        }

        public RunningEntry GetRunningEntry()
        {
            return new RunningEntry
            {
                Date = new DateTime(2020, 05, 10, 08, 30, 00),
                RunningDistance = 5,
                RunningPace = 7.15,
                RunningId = 1,
                RunningTime = 35.75,
                RunningComments = "Test!"
            };
        }

        public async Task<List<ActivitySummary>> GetRunningEntries()
        {
            var authenticator = await _stravaAuthenticator.CreateAuthenticatorAsync();

            var activities = await _stravaClientWrapper.GetAthleteActivities(authenticator);

            return activities.Where(x => x.Type == ActivityType.Run).ToList();
        }
    }
}

