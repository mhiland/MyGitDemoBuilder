using GitDemo.DTO;

namespace GitDemo.UnitTests
{
    public class BlobObjectTests
    {
        private BlobObject EmptyBlobObject { get; set; }

        [OneTimeSetUp]
        public void OneTimeSetUp() 
        {
            EmptyBlobObject = CreateTestBlobObject();
        }

        private static BlobObject CreateTestBlobObject()
        {
            var blogObject = new BlobObject();
            blogObject.SetHash();

            return blogObject;
        }

        [Test]
        public void Verify_EmptyBlobObject_HasExpectedHash()
        {
            const string expected = "E69DE29BB2D1D6434B8B29AE775AD8C2E48C5391";

            Assert.AreEqual(expected, EmptyBlobObject.Hash);
        }

        [Test]
        public void Verify_EmptyBlobObject_HasExpectedFileFormat()
        {
            const string expected = "blob 0\0";

            Assert.AreEqual(expected, EmptyBlobObject.GetFileContent());
        }

        [Test]
        public void Verify_EmptyBlobObject_HasCorrectedFileFormat()
        {
            const string expectedHash = "BC9F3DE89F680D2185A9E0CD316C29B2406DB8EF";
            const string expectedFileContent = "blob 14\0initial Commit";

            var blogObject = new BlobObject()
            {
                Content = "initial Commit"
            };
            blogObject.SetHash();

            Assert.AreEqual(expectedFileContent, blogObject.GetFileContent());
            Assert.AreEqual(expectedHash, blogObject.Hash);
        }
    }
}
