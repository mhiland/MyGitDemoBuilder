using System.Collections.Generic;
using GitDemo.DTO;
using NodaTime;

namespace GitDemo.GitBuilder
{
    public class ObjectBuilder : Base
    {
        public List<TreeObject> TreeObjects { get; } = new List<TreeObject>();
        public List<BlobObject> BlobObjects { get; } = new List<BlobObject>();
        public List<GitLog> GitLog { get; } = new List<GitLog>();
        public List<CommitObject> CommitObjects { get; } = new List<CommitObject>();
        public const string BlobContent = "";
        public string PreviousCommit { get; set; } = "0000000000000000000000000000000000000000";
        public string TimeStamp { get; set; }
        private string CurrentCommit { get; set; }

        public void CreateObjects()
        {
            foreach (var commitDate in CommitDates)
            {
                AddCommit(commitDate);
            }
        }

        public void AddCommit(Instant commitDate)
        {
            TimeStamp = CreateGitTimeStamp(commitDate);

            var blobObject = CreateBlogObject(BlobContent);
            var treeObject = CreateTreeObject(blobObject); 
            CurrentCommit = CreateCommitObject(treeObject.Hash); //todo "28f24003920c1495d9558eff7b4453c7ddc87fb2"
            CreateCommitLog();

            PreviousCommit = CurrentCommit;
            CommitCounter++;
        }

        public void CreateCommitLog()
        {
            var gitLog = new GitLog
            {
                CommitMessage = CreateCommitMessage(),
                CurrentCommit = CurrentCommit,
                GitEmail = UserEmail,
                GitUser = UserName,
                PreviousCommit = PreviousCommit,
                TimeStamp = TimeStamp
            };

            GitLog.Add(gitLog);
        }

        public string CreateCommitObject(string treeHash)
        {
            var user = new User
            {
                UserName = UserName,
                UserEmail = UserEmail,
                TimeStamp = TimeStamp
            };

            var commitObject = new CommitObject
            {
                CommitMessage = $"Commit {CommitCounter}",
                Author = user,
                Committer = user,
                Parent = PreviousCommit,
                Tree = treeHash
            };
            commitObject.SetHash();
            CommitObjects.Add(commitObject);

            return commitObject.Hash;
        }

        public BlobObject CreateBlogObject(string content)
        {
            var blogObject = new BlobObject
            {
                Content = content
            };

            blogObject.SetHash();

            BlobObjects.Add(blogObject);

            return blogObject;
        }

        public TreeObject CreateTreeObject(BlobObject blobObject)
        {
            var treeObject = new TreeObject
            {
                Permissions = "100644",
                ObjectType = "blob",
                ObjectFileName = ExampleFileName,
                BlobObject = blobObject
            };

            treeObject.SetHash();

            TreeObjects.Add(treeObject);

            return treeObject;
        }
    }
}
