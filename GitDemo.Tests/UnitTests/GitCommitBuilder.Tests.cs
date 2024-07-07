using GitDemo.GitBuilder;
using NodaTime;
using NUnit.Framework;

namespace GitDemo.UnitTests
{
    public class GitCommitBuilderTests
    {
        private ObjectWriter GitCommitBuilder { get; set; }

        [OneTimeSetUp]
        public void Setup()
        {
            GitCommitBuilder = new ObjectWriter();   
        }

        [Test]
        public void Verify_InitialGitCommit_HasZeroedGuid()
        {
            const string zeroedSha = "0000000000000000000000000000000000000000";
            var timeStamp = Instant.FromUtc(2018, 4, 5, 18, 41);
            const string expectedTimeStamp = "1522953660 -0700";

            GitCommitBuilder.AddCommit(timeStamp);

            var gitLogHead = GitCommitBuilder.GetGitHead();

            Assert.AreEqual(gitLogHead[0].PreviousCommit, zeroedSha);
            Assert.AreEqual(gitLogHead[0].CommitMessage, "commit (initial): Commit 0");
            Assert.AreEqual(expectedTimeStamp, gitLogHead[0].TimeStamp);
        }

        [Test]
        public void Verify_SecondCommitHasSamePreviousCommit_AsPreviousCurrentCommit()
        {
            var timeStamp = Instant.FromUtc(2018, 4, 5, 18, 41);
            GitCommitBuilder.AddCommit(timeStamp);
            GitCommitBuilder.AddCommit(timeStamp.Plus(Duration.FromHours(1)));

            var gitLogHead = GitCommitBuilder.GetGitHead();

            Assert.AreEqual(gitLogHead[1].PreviousCommit, gitLogHead[0].CurrentCommit);
        }
    }
}
