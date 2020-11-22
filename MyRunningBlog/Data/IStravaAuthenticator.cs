using RestSharp.Portable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MyRunningBlog.Data.RunningRepository;

namespace MyRunningBlog.Data
{
    public interface IStravaAuthenticator
    {
        Task<IAuthenticator> CreateAuthenticatorAsync();
    }
}
