using System.Collections.Generic;
using System.IO;
using System;
using GitDemo.GitBuilder;
using NodaTime;

namespace GitDemo
{
    public class CreatePatternBasedGitHistory
    {
        public static void Run(FileInfo patternFile, DirectoryInfo gitDirectory, string gitUserName, string gitUserEmail)
        {
            var fullPattern = GetPatternFromDefinitionFile(patternFile);

            var gitHistory = GetDatesFromPattern(fullPattern);

            Console.WriteLine($"Initialize git directory: {gitDirectory}");
            // Delete the folder if it exists
            if (Directory.Exists(gitDirectory.FullName))
            {
                Directory.Delete(gitDirectory.FullName, true);
                Console.WriteLine(" git folder deleted.");
            }

            // Recreate the folder
            Directory.CreateDirectory(gitDirectory.FullName);
            Console.WriteLine(" git folder created.");

            CreatePatternInGitRepo(gitDirectory, gitHistory, gitUserName, gitUserEmail);
        }

        private static char[,] GetPatternFromDefinitionFile(FileInfo patternFile)
        {
            var dataDefinition = new DataDefinition();
            dataDefinition.ExtractGridFromFile(patternFile);
            var fullPattern = dataDefinition.ExpandExampleGridToFullSize();
            return fullPattern;
        }

        private static List<Instant> GetDatesFromPattern(char[,] fullGrid)
        {
            var dateComposer = new DateComposer();
            var dateList = dateComposer.GetDateFromPattern(fullGrid);
            return dateList;
        }

        private static void CreatePatternInGitRepo(DirectoryInfo gitDirectory, List<Instant> gitHistory, string gitUserName, string gitUserEmail)
        {
            var commitBuilder = new ObjectWriter();
            commitBuilder.SetBase(gitDirectory, gitHistory, gitUserName, gitUserEmail);
            commitBuilder.CreateObjects();
            commitBuilder.WriteGitDirectory();
        }
    }
}
