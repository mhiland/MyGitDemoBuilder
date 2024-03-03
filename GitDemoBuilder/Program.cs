using System.IO;
using Microsoft.Extensions.Configuration;

namespace GitDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

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
