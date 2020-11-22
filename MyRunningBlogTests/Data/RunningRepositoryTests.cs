using AutoFixture;
using AutoFixture.Kernel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyRunningBlog.Data;
using RestSharp.Portable;
using StravaSharp;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyRunningBlogTests
{
    [TestClass]
    public class RunningRepositoryTests
    {
        private readonly RunningRepository sut;
        private readonly Mock<IStravaAuthenticator> _stravaAuthenticatorMock;
        private readonly Mock<IAuthenticator> _authenticatorMock;
        private readonly Fixture _fixture;
        private readonly Mock<IStravaClientWrapper> _stravaClientWraperMock;

        public RunningRepositoryTests()
        {
            _stravaAuthenticatorMock = new Mock<IStravaAuthenticator>();
            _authenticatorMock = new Mock<IAuthenticator>();
            _stravaAuthenticatorMock.Setup(sa => sa.CreateAuthenticatorAsync()).ReturnsAsync(_authenticatorMock.Object);
            _stravaClientWraperMock = new Mock<IStravaClientWrapper>();

            _fixture = new Fixture();
            sut = new RunningRepository(_stravaAuthenticatorMock.Object, _stravaClientWraperMock.Object);
        }

        [TestMethod]
        public async Task GetRunningEntries_ReturnsListOfRunningActivitySummaries()
        {
            var athleteActivitiesReturned = _fixture.CreateMany<ActivitySummary>().ToList();

            var runningActivity = new Fixture()
              .For<ActivitySummary>()
              .With(x => x.Type, ActivityType.Run)
              .Create();

            athleteActivitiesReturned.Add(runningActivity);

            _stravaClientWraperMock.Setup(c => c.GetAthleteActivities(_authenticatorMock.Object)).ReturnsAsync(athleteActivitiesReturned);
            var result = await sut.GetRunningEntries();

            Assert.IsNotNull(result);
            Assert.AreEqual(result[0].AverageSpeed, athleteActivitiesReturned[0].AverageSpeed);
            Assert.AreEqual(result[0].Distance, athleteActivitiesReturned[0].Distance);
            Assert.AreEqual(result[0].MovingTime, athleteActivitiesReturned[0].MovingTime);
            Assert.AreEqual(result[0].StartDate, athleteActivitiesReturned[0].StartDate);
            Assert.AreEqual(result[0].Type, athleteActivitiesReturned[0].Type);
        }

        //[TestMethod]
        //public async void GetRunningEntries_ReturnsCorrectListOfRunningEntries()
        //{
        //    var result = await sut.GetRunningEntries();

        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(new DateTime(2020, 05, 10, 08, 30, 00), result[0].Date);
        //    Assert.AreEqual("Test!", result[0].RunningComments);
        //    Assert.AreEqual(35.75, result[0].RunningTime);
        //    Assert.AreEqual(1, result[0].RunningId);
        //    Assert.AreEqual(7.15, result[0].RunningPace);
        //}
    }

    public class FixtureCustomization<T>
    {
        public Fixture Fixture { get; }

        public FixtureCustomization(Fixture fixture)
        {
            Fixture = fixture;
        }

        public FixtureCustomization<T> With<TProp>(Expression<Func<T, TProp>> expr, TProp value)
        {
            Fixture.Customizations.Add(new OverridePropertyBuilder<T, TProp>(expr, value));
            return this;
        }

        public T Create() => Fixture.Create<T>();
    }

    public class OverridePropertyBuilder<T, TProp> : ISpecimenBuilder
    {
        private readonly PropertyInfo _propertyInfo;
        private readonly TProp _value;

        public OverridePropertyBuilder(Expression<Func<T, TProp>> expr, TProp value)
        {
            _propertyInfo = (expr.Body as MemberExpression)?.Member as PropertyInfo ??
                            throw new InvalidOperationException("invalid property expression");
            _value = value;
        }

        public object Create(object request, ISpecimenContext context)
        {
            var pi = request as ParameterInfo;
            if (pi == null)
                return new NoSpecimen();

            var camelCase = Regex.Replace(_propertyInfo.Name, @"(\w)(.*)",
                m => m.Groups[1].Value.ToLower() + m.Groups[2]);

            if (pi.ParameterType != typeof(TProp) || pi.Name != camelCase)
                return new NoSpecimen();

            return _value;
        }
    }

    public static class CompositionExt
    {
        public static FixtureCustomization<T> For<T>(this Fixture fixture)
            => new FixtureCustomization<T>(fixture);
    }
}
