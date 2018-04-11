using System.Collections.Generic;
using System.IO;
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

            CreatePatternInGitRepo(gitDirectory, gitHistory, gitUserName, gitUserEmail);
        }

        private static void CreatePatternInGitRepo(DirectoryInfo gitDirectory, List<Instant> gitHistory, string gitUserName, string gitUserEmail)
        {
            var commitBuilder = new ObjectWriter();
            commitBuilder.SetBase(gitDirectory, gitHistory, gitUserName, gitUserEmail);
            commitBuilder.CreateObjects();
            commitBuilder.WriteGitDirectory();
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
    }
}
