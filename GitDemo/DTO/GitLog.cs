namespace GitDemo.DTO
{
    public class GitLog
    {
        public string PreviousCommit { get; set; }
        public string CurrentCommit { get; set; }
        public string GitUser { get; set; }
        public string GitEmail { get; set; }
        public string TimeStamp { get; set; }
        public string CommitMessage { get; set; }

        public string GetFormattedLog()
        {
            return $"{PreviousCommit} {CurrentCommit} {GitUser} <{GitEmail}> {TimeStamp}\t{CommitMessage}";
        }
    }
}
