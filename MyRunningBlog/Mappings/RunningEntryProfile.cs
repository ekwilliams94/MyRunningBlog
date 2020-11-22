using AutoMapper;
using MyRunningBlog.Models;
using StravaSharp;

namespace MyRunningBlog.Mappings
{
    public class RunningEntryProfile: Profile
    {
        public RunningEntryProfile()
        {
            CreateMap<ActivitySummary, RunningEntry>()
                .ForMember(dest => dest.RunningDistance, opt => opt.MapFrom(src => src.Distance))
                .ForMember(dest => dest.RunningPace, opt => opt.MapFrom(src => src.AverageSpeed))
                .ForMember(dest => dest.RunningTime, opt => opt.MapFrom(src => src.MovingTime))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.StartDate));
        }
    }
}
