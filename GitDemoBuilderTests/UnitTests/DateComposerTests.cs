using NodaTime;

namespace GitDemo.UnitTests
{
    public class DateComposerTests
    {
        private DateComposer _dateComposer;
        private Instant StartDate { get; set; }  
        private char[,] ExpectedGrid { get; set; }

        [OneTimeSetUp]
        public void Setup()
        {
            StartDate = Instant.FromUtc(2012, 3, 7, 12, 0);
            _dateComposer = new DateComposer(StartDate);

            var rows = new string[1];
            rows[0] = "0100000010";

            ExpectedGrid = new char[rows.Length, rows[0].Length];
            for (var i = 0; i < rows.Length; i++)
            for (var j = 0; j < rows[i].Length; j++)
            {
                ExpectedGrid[i, j] = rows[i][j];
            }
        }

        [Test]
        public void TestDataComposition()
        {
            var expectedDates = new List<Instant>
                {
                    Instant.FromUtc(2012, 3, 14, 12, 0),
                    Instant.FromUtc(2012, 5, 2, 12, 0)
                };

            var actualDates = _dateComposer.GetDateFromPattern(ExpectedGrid);

            Assert.That(actualDates, Is.EqualTo(expectedDates));
        }

        [Test]
        public void Should_HaveCorrectNodaStartTime()
        {
            var expectedValue = Instant.FromUtc(2012, 3, 7, 12, 0);
            var dateComposer = new DateComposer();

            Assert.That(expectedValue, Is.EqualTo(dateComposer.StartDate));
        }
    }
}
