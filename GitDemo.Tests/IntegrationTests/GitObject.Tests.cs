using System.IO;
using System.Text;
using NUnit.Framework;

namespace GitDemo.UnitTests.IntegrationTests
{
    public class GitObjectTests : TestFixtureBase
    {
        [Test]
        public void Verify_EmptyBlobObject_IsCompressedAsExpected()
        {
            var file = Path.Combine(GitDirectory.FullName, "objects\\E6", "9DE29BB2D1D6434B8B29AE775AD8C2E48C5391");
            Compression.DecompressData(File.ReadAllBytes(file), out var outData);
            var actualData = Encoding.UTF8.GetString(outData);

            const string expectedData = "blob 0\0";

            Assert.AreEqual(expectedData, actualData);
        }

        [Test]
        public void Verify_TreeObject_IsAsExpected()
        {
            var file = Path.Combine(GitDirectory.FullName, "objects\\4D", "E1EF196746A289866677488A7081869EA1A593");
            Compression.DecompressData(File.ReadAllBytes(file), out var outData);
            var actualData = Encoding.UTF8.GetString(outData);

            const string expectedData = "tree 63\0100644 exampleFile.txt\0e69de29bb2d1d6434b8b29ae775ad8c2e48c5391";

            Assert.AreEqual(expectedData, actualData);
        }

        [Test]
        public void Verify_FirstCommitObject_IsAsExpected()
        {
            var file = Path.Combine(GitDirectory.FullName, "objects\\3C", "D20A70F3BEC41ECBC47EB8434F0B9197601310");

            Compression.DecompressData(File.ReadAllBytes(file), out var outData);

            var actual = Encoding.UTF8.GetString(outData);

            const string expected =
                "commit 207\x00tree 4DE1EF196746A289866677488A7081869EA1A593\nauthor FirstName.LastName <FirstName.LastName@email.com> 1523316720 -0700\ncommitter FirstName.LastName <FirstName.LastName@email.com> 1523316720 -0700\n\nCommit 0\n";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Verify_LogsHead_IsAsExpected()
        {
            var file = Path.Combine(GitDirectory.FullName, "logs", "HEAD");

            var actual = File.ReadAllBytes(file);

            const string expected =
                "0000000000000000000000000000000000000000 3CD20A70F3BEC41ECBC47EB8434F0B9197601310 FirstName.LastName <FirstName.LastName@email.com> 1523316720 -0700\tcommit (initial): Commit 0\r\n" +
                "3CD20A70F3BEC41ECBC47EB8434F0B9197601310 0DB50E0D48F0A7A121CE592487E0603E5268713C FirstName.LastName <FirstName.LastName@email.com> 1523323920 -0700\tcommit: Commit 1\r\n";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Verify_LogsHeadMaster_IsAsExpected()
        {
            var file = Path.Combine(GitDirectory.FullName, "logs\\refs\\heads", "master");

            var actual = File.ReadAllBytes(file);

            const string expected =
                "0000000000000000000000000000000000000000 3CD20A70F3BEC41ECBC47EB8434F0B9197601310 FirstName.LastName <FirstName.LastName@email.com> 1523316720 -0700\tcommit (initial): Commit 0\r\n" +
                "3CD20A70F3BEC41ECBC47EB8434F0B9197601310 0DB50E0D48F0A7A121CE592487E0603E5268713C FirstName.LastName <FirstName.LastName@email.com> 1523323920 -0700\tcommit: Commit 1\r\n";

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Verify_RefsHeadsMaster_IsAsExpected()
        {
            var file = Path.Combine(GitDirectory.FullName, "refs\\heads", "master");

            var actual = Encoding.UTF8.GetString(File.ReadAllBytes(file)); 

            const string expected =
                "0DB50E0D48F0A7A121CE592487E0603E5268713C\r\n";
     
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Verify_SecondCommitObject_IsAsExpected()
        {
            var file = Path.Combine(GitDirectory.FullName, "objects\\0D", "B50E0D48F0A7A121CE592487E0603E5268713C");

            Compression.DecompressData(File.ReadAllBytes(file), out var outData);

            var actual = Encoding.UTF8.GetString(outData);

            const string expected =
                           "commit 255\x00tree 4DE1EF196746A289866677488A7081869EA1A593\nparent 3CD20A70F3BEC41ECBC47EB8434F0B9197601310\nauthor FirstName.LastName <FirstName.LastName@email.com> 1523323920 -0700\ncommitter FirstName.LastName <FirstName.LastName@email.com> 1523323920 -0700\n\nCommit 1\n";

            Assert.AreEqual(expected, actual);
        }
    }
}
