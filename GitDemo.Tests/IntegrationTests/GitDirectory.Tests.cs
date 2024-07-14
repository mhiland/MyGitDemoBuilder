using System.IO;
using System.IO.Compression;
using NUnit.Framework;

namespace GitDemo.UnitTests.IntegrationTests
{
    [TestFixture]
    public class GitDirectoryTests : TestFixtureBase
    {

        [Test]
        public void GitDirectoryExists()
        {
            Assert.That(Directory.Exists(GitDirectory.FullName), Is.True, ".git directory does not exist");
        }

        [Test]
        public void HasConfigFile()
        {
            string configPath = Path.Combine(GitDirectory.FullName, ".git", "config");
            Assert.That(File.Exists(configPath), Is.True, "config file does not exist");
        }

        [Test]
        public void HasHEADFile()
        {
            string headPath = Path.Combine(GitDirectory.FullName, ".git", "HEAD");
            Assert.That(File.Exists(headPath), Is.True, "HEAD file does not exist");
        }

        [Test]
        public void HasObjectsFolder()
        {
            string objectsPath = Path.Combine(GitDirectory.FullName, ".git", "objects");
            Assert.That(Directory.Exists(objectsPath), Is.True, "objects folder does not exist");
        }

        [Test]
        public void HasRefsFolder()
        {
            string refsPath = Path.Combine(GitDirectory.FullName, ".git", "refs");
            Assert.That(Directory.Exists(refsPath), Is.True, "refs folder does not exist");
        }

        [Test]
        public void HasDescriptionFile()
        {
            string descriptionPath = Path.Combine(GitDirectory.FullName, ".git", "description");
            Assert.That(File.Exists(descriptionPath), Is.True, "description file does not exist");
        }

        [Test]
        public void VerifyHEADContent()
        {
            string headPath = Path.Combine(GitDirectory.FullName, ".git", "HEAD");
            string content = File.ReadAllText(headPath);
            Assert.That(content.StartsWith("ref: refs/heads/"), Is.True, "HEAD file does not point to a branch");
        }

        [Test]
        public void VerifyConfigContent()
        {
            string configPath = Path.Combine(GitDirectory.FullName, ".git", "config");
            string content = File.ReadAllText(configPath);
            Assert.That(content.Contains("[core]"), Is.True, "config file is missing [core] section");
        }

        [Test]
        public void VerifyRefsHeadsFolder()
        {
            string refsHeadsPath = Path.Combine(GitDirectory.FullName, ".git", "refs", "heads");
            Assert.That(Directory.Exists(refsHeadsPath), Is.True, "refs/heads folder does not exist");
        }

        [Test]
        public void VerifyRefsTagsFolder()
        {
            string refsTagsPath = Path.Combine(GitDirectory.FullName, ".git", "refs", "tags");
            Assert.That(Directory.Exists(refsTagsPath), Is.True, "refs/tags folder does not exist");
        }

        [Test]
        public void VerifyInfoExcludeFile()
        {
            string infoExcludePath = Path.Combine(GitDirectory.FullName, ".git", "info", "exclude");
            Assert.That(File.Exists(infoExcludePath), Is.True, "info/exclude file does not exist");
        }

        [Test]
        public void VerifyLogsFolder()
        {
            string logsPath = Path.Combine(GitDirectory.FullName, ".git", "logs");
            Assert.That(Directory.Exists(logsPath), Is.True, "logs folder does not exist");
        }

        [Test]
        public void VerifyCommitsInLogsHEAD()
        {
            string logsHEADPath = Path.Combine(GitDirectory.FullName, ".git", "logs", "HEAD");
            Assert.That(File.Exists(logsHEADPath), Is.True, "logs/HEAD file does not exist");

            string[] lines = File.ReadAllLines(logsHEADPath);
            Assert.That(lines.Length > 0, Is.True, "logs/HEAD file is empty");
        }

        [Test]
        public void VerifyDescriptionFileContent()
        {
            string descriptionPath = Path.Combine(GitDirectory.FullName, ".git", "description");
            string content = File.ReadAllText(descriptionPath);
            Assert.That(string.IsNullOrEmpty(content), Is.False, "description file is empty");
        }
    }
}
