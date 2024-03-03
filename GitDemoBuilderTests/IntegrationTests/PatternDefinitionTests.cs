﻿using System.Text;

namespace GitDemo.UnitTests.IntegrationTests
{
    public class PatternDefinitionTests
    {
        private readonly FileInfo _fileInfo = new FileInfo(Path.Combine(TestContext.CurrentContext.TestDirectory, "../../../../", "GitDemoBuilder", "PatternDefinitions", "m.txt"));

        private readonly char[,] _expectedGrid = new char[7, 10];
        private DataDefinition DataDefinition { get; set; }
        private char[,] ActualGrid { get; set; }

        [OneTimeSetUp]
        public void Setup()
        {
            DataDefinition = new DataDefinition(_fileInfo);
            ActualGrid = DataDefinition.ExtractGridFromFile();

            var rows = new string[7];
            rows[0] = "0440000440";
            rows[1] = "0404004040";
            rows[2] = "0404040040";
            rows[3] = "0400400040";
            rows[4] = "0400000040";
            rows[5] = "0000000000";
            rows[6] = "0123443210";

            for (var i = 0; i < rows.Length; i++)
            for (var j = 0; j < rows[i].Length; j++)
            {
                _expectedGrid[i, j] = rows[i][j];
            }
        }

        [Test]
        public void Setup_DefinitionArray_HasRightSize()
        {
            Assert.That(ActualGrid, Is.EqualTo(_expectedGrid));
        }

        [Test]
        public void Verify_ExpandedFirstRow_HasExpectedValue()
        {
            const int row = 0;
            const string expectedFirstRow = "04400004400440000440044000044004400004400440000440";

            var actualGridFullSize = DataDefinition.ExpandExampleGridToFullSize();
            var actualFirstRow = new StringBuilder();
            var period = DataDefinition.GetExtendedPeriod();
            for (var j = 0; j < period; j++)
            {
                actualFirstRow.Append(actualGridFullSize[row,j]);
            }

            Assert.That(actualFirstRow.ToString(), Is.EqualTo(expectedFirstRow));
        }

        [Test]
        public void Should_HaveCorrectNodaStartTime()
        {
            var expectedValue = 50;
            var period = DataDefinition.GetExtendedPeriod();

            Assert.That(expectedValue, Is.EqualTo(period));
        }
    }
}
