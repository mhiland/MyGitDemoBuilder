using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.IO;
using System;

namespace GitDemo
{
    public class Program
    {
        private static IConfiguration _configuration;

        public static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json");

            _configuration = builder.Build();

            string userEmail = _configuration["AppSettings:GitUserEmail"];
            string userName = _configuration["AppSettings:GitUserName"];

            string currentDirectory = Directory.GetCurrentDirectory();
            string patternFilename = _configuration["AppSettings:PatternDefinition"];
            string patternFilePath = Path.Combine(currentDirectory, "PatternDefinitions", patternFilename);
            var patternFile = new FileInfo(patternFilePath);


            string gitDirectoryPath = _configuration["AppSettings:GitDirectory"];
            var gitDirectoryInfo = new DirectoryInfo(gitDirectoryPath);

            CreatePatternBasedGitHistory.Run(patternFile, gitDirectoryInfo, userName, userEmail);
        }
    }
}
