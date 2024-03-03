using Microsoft.Extensions.Configuration;
using NodaTime;
using NodaTime.Text;

namespace GitDemo.UnitTests.IntegrationTests
{
    [TestFixture]
    public class ConfigurationTests
    {
        private IConfigurationRoot _configuration;

        [SetUp]
        public void SetUp()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
        }

        [Test]
        public void Should_HaveCorrectUserEmailSetting()
        {
            var expectedValue = "FirstName.LastName@email.com";

            Assert.That(expectedValue, Is.EqualTo(_configuration["AppSettings:GitUserEmail"]));
        }

        [Test]
        public void Should_HaveCorrectNodaTime()
        {
            var expectedValue = Instant.FromUtc(2012, 3, 7, 12, 0);;
            var startTimeString = _configuration["AppSettings:StartInstant"];
            var startTimeInstant = InstantPattern.General.Parse(startTimeString).Value;

            Assert.That(expectedValue, Is.EqualTo(startTimeInstant));
        }
    }
}
