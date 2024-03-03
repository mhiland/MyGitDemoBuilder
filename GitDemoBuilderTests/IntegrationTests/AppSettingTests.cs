using Microsoft.Extensions.Configuration;

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
        public void Should_HaveCorrectConnectionString()
        {
            var expectedValue = "FirstName.LastName@email.com";

            Assert.That(expectedValue, Is.EqualTo(_configuration["AppSettings:GitUserEmail"]));
        }
    }
}
