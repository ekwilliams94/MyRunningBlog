using MyRunningBlog.Models;
using StravaSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyRunningBlog.Data
{
    public interface IRunningRepository
    {
        RunningEntry GetRunningEntry();
        Task<List<ActivitySummary>> GetRunningEntries();
    }
}
