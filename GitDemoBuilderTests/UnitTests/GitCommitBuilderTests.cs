using GitDemo.GitBuilder;
using NodaTime;

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

            Assert.That(gitLogHead[0].PreviousCommit, Is.EqualTo(zeroedSha));
            Assert.That(gitLogHead[0].CommitMessage, Is.EqualTo("commit (initial): Commit 0"));
            Assert.That(gitLogHead[0].TimeStamp, Is.EqualTo(expectedTimeStamp));
        }

        [Test]
        public void Verify_SecondCommitHasSamePreviousCommit_AsPreviousCurrentCommit()
        {
            var timeStamp = Instant.FromUtc(2018, 4, 5, 18, 41);
            GitCommitBuilder.AddCommit(timeStamp);
            GitCommitBuilder.AddCommit(timeStamp.Plus(Duration.FromHours(1)));

            var gitLogHead = GitCommitBuilder.GetGitHead();

            Assert.That(gitLogHead[0].CurrentCommit, Is.EqualTo(gitLogHead[1].PreviousCommit));
        }
    }
}
