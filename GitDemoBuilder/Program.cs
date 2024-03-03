using System.IO;
using Microsoft.Extensions.Configuration;

namespace GitDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .Build();

            var userEmail = configuration["AppSettings:GitUserEmail"];
            var userName = configuration["AppSettings:GitUserName"];
            var gitDirectoryPath = configuration["AppSettings:GitDirectory"];
            var patternFilename = configuration["AppSettings:PatternDefinition"];
            
            var patternFile = new FileInfo(Path.Combine("PatternDefinitions", patternFilename));
            var gitDirectoryInfo = new DirectoryInfo(gitDirectoryPath);

            CreatePatternBasedGitHistory.Run(patternFile, gitDirectoryInfo, userName, userEmail);
        }
    }
}
