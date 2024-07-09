using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace GitDemo.DTO
{
    public abstract class Base
    {
        public byte[] ByteArray { get; private set; }
        public string Hash { get; private set; }
        protected string CommitFileContents { get; set; }
        protected string FileName { get; set; }
        protected string FolderName { get; set; }

        public abstract string GetFileContent();

        public void SetHash()
        {
            CommitFileContents = GetFileContent();
            var sha1 = SHA1.Create();
            var bytes = Encoding.UTF8.GetBytes(CommitFileContents);
            ByteArray = sha1.ComputeHash(bytes);
            Hash = GetStringFromHash(ByteArray);

            FolderName = Hash.Substring(0, 2);
            FileName = Hash.Substring(2, 38);
        }

        public void WriteFile(string objectDirectory)
        {
            var folder = Path.Combine(objectDirectory, FolderName);
            var filePath = new FileInfo(Path.Combine(folder, FileName));

            Directory.CreateDirectory(folder);

            var compression = new ZCompression();
            var compressedFileContents = compression.Compress(CommitFileContents);
            compression.WriteOut(filePath, compressedFileContents);
        }

        protected static string GetStringFromHash(byte[] hash)
        {
            var result = new StringBuilder();
            foreach (var t in hash)
            {
                result.Append(t.ToString("X2"));
            }
            return result.ToString();
        }
    }
}
