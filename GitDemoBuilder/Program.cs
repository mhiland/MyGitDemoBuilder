using System.Configuration;
using System.IO;

namespace GitDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var userEmail = ConfigurationManager.AppSettings.Get("GitUserEmail");
            var userName = ConfigurationManager.AppSettings.Get("GitUserName");
            var gitDirectoryPath = ConfigurationManager.AppSettings.Get("GitDirectory");
            var patternFilename = ConfigurationManager.AppSettings.Get("PatternDefinition");
            var patternFile = new FileInfo($"PatternDefinitions\\{patternFilename}");
            var gitDirectoryInfo = new DirectoryInfo(gitDirectoryPath);

            CreatePatternBasedGitHistory.Run(patternFile, gitDirectoryInfo, userName, userEmail);
        }
    }
}
