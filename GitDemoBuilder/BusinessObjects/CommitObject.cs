namespace GitDemo.DTO
{
    public class CommitObject : Base
    {
        public string Tree { get; set; }
        public string Parent { get; set; }
        public User Author { get; set; }
        public User Committer { get; set; }
        public string CommitMessage { get; set; }

        public override string GetFileContent()
        {
            var str = $"tree {Tree}\n" +
                   AddParent() +
                   $"author {Author.UserName} <{Author.UserEmail}> {Author.TimeStamp}\n" +
                   $"committer {Author.UserName} <{Author.UserEmail}> {Author.TimeStamp}\n\n" +
                   $"{CommitMessage}\n";

            return $"commit {str.Length}\0{str}";
        }

        private string AddParent()
        {
            return Parent == "0000000000000000000000000000000000000000" ? "" : $"parent {Parent}\n";
        }
    }
}
