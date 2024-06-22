using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using NodaTime;

namespace GitDemo.GitBuilder
{
    public class Base
    {
        public DirectoryInfo GitFolderInfo { get; set; }
        public const string LogsHeadFile = @".git/logs/HEAD";
        public const string LogsMasterFile = @".git/logs/refs/heads/master";
        public const string RefsMasterFile = @".git/refs/heads/master";
        public const string ObjectsFolder = @".git/objects";
        public const string ExampleFileName = "exampleFile.txt";
        
        public List<Instant> CommitDates { get; set; }
        public int CommitCounter { get; set; }
        public string UserEmail { get; set; }
        public string UserName { get; set; }

        public void SetBase(DirectoryInfo gitFolder)
        {
            GitFolderInfo = gitFolder;
        }

        public void SetBase(DirectoryInfo gitFolder, List<Instant> commitDates, string userName, string userEmail)
        {
            GitFolderInfo = gitFolder;
            CommitDates = commitDates;
            UserEmail = userEmail;
            UserName = userName;
        }

        public static string GenerateSha1String(string inputString)
        {
            var sha1 = SHA1.Create();
            var bytes = Encoding.UTF8.GetBytes(inputString);
            var hash = sha1.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }

        private static string GetStringFromHash(byte[] hash)
        {
            var result = new StringBuilder();
            foreach (var t in hash)
            {
                result.Append(t.ToString("X2"));
            }
            return result.ToString();
        }

        public string CreateCommitMessage()
        {
            return CommitCounter == 0 ? $"commit (initial): Commit {CommitCounter}" : $"commit: Commit {CommitCounter}";
        }

        public static string CreateGitTimeStamp(Instant timeStamp)
        {
            var builder = $"{timeStamp.ToUnixTimeTicks().ToString().Substring(0, 10)} -0700";
            return builder;
        }
    }
}
