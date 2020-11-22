using AutoFixture;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyRunningBlog.Mappings;
using MyRunningBlog.Models;
using StravaSharp;

namespace MyRunningBlogTests.Mapping
{
    [TestClass]
    public class RunningEntryMapperTests
    {
        private readonly IMapper sut;
        private readonly IFixture _fixture;

        public RunningEntryMapperTests()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new RunningEntryProfile());
            });

            sut = new Mapper(mapperConfig);

            _fixture = new Fixture();                       
        }

        [TestMethod]
        public void Maps_ActivitySummary_To_RunningEntry()
        {
            var input = _fixture.Create<ActivitySummary>();

            var result = sut.Map<RunningEntry>(input);

            Assert.AreEqual(input.Distance, result.RunningDistance);
            Assert.AreEqual(input.AverageSpeed, result.RunningPace);
            Assert.AreEqual(input.MovingTime, result.RunningTime);
            Assert.AreEqual(input.StartDate, result.Date);

        }
    }
}
