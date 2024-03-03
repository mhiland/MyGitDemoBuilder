﻿using GitDemo.GitBuilder;
using NodaTime;

namespace GitDemo.UnitTests.IntegrationTests
{
    [TestFixture]
    public abstract class TestFixtureBase
    {
        public static List<Instant> PublishDates;
        public DirectoryInfo GitDirectory { get; set; }
        private const string DirectoryName = "GitUnitTests";
        private const string UserEmail = "FirstName.LastName@email.com";
        private const string UserName = "FirstName.LastName";

        [OneTimeSetUp]
        public void Setup()
        {
            CreateGitDirectory();

            CreateTestDates();

            var parentDirectory = GitDirectory.Parent;
            if (parentDirectory != null)
            {
                CreatePatternInGitRepo(parentDirectory, PublishDates, UserName, UserEmail);
            }
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            GitDirectory.Parent?.Delete(true);
        }

        private void CreateGitDirectory()
        {
            var name = Path.Combine(Path.GetTempPath(), DirectoryName, ".git");
            GitDirectory = new DirectoryInfo(name);

            if (GitDirectory.Exists)
            {
                GitDirectory.Delete(true);
            }

            GitDirectory = Directory.CreateDirectory(name);
        }

        private static void CreateTestDates()
        {
            var now = new DateTime(2018, 4, 09, 16, 32, 0);
            var instant = Instant.FromDateTimeUtc(now.ToUniversalTime());

            PublishDates = new List<Instant>
            {
                instant,
                Instant.FromDateTimeUtc(now.AddHours(2).ToUniversalTime())
            };
        }

        private static void CreatePatternInGitRepo(DirectoryInfo gitDirectory, List<Instant> dateList, string userName, string userEmail)
        {
            var commitBuilder = new ObjectWriter();
            commitBuilder.SetBase(gitDirectory, dateList, userName, userEmail);
            commitBuilder.CreateObjects();
            commitBuilder.WriteGitDirectory();
        }
    }
}
