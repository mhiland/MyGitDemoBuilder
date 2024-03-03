using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace GitDemo
{
    public class DataDefinition
    {
        private const int DefinitionRows = 7;
        private readonly int DefinitionColumns;
        private readonly int MaxNumberOfColumns;
        private readonly char[,] _grid;

        public DataDefinition()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();
            var t = configuration["AppSettings:PartialPatternSize"];

            DefinitionColumns = int.Parse(configuration["AppSettings:PartialPatternSize"]);
            MaxNumberOfColumns = int.Parse(configuration["AppSettings:FullPatternSize"]);

            _grid = new char[DefinitionRows, DefinitionColumns];
        }

        public char[,] ExtractGridFromFile(FileInfo file)
        {
            if (!file.Exists)
            {
                throw new Exception("Pattern definition does not exist");
            }

            var i = 0;

            using (var sr = file.OpenText())
            {
                string row;
                while ((row = sr.ReadLine()) != null)
                {
                    for (var j = 0; j < row.Length; j++)
                    {
                        _grid[i, j] = row[j];
                    }
                    i++;
                }
            }

            return _grid;
        }

        public char[,] ExpandExampleGridToFullSize()
        {
            var fullGrid = new char[DefinitionRows, MaxNumberOfColumns];

            for (var y = 0; y < MaxNumberOfColumns ; y++)
            {
                var ptr = y % DefinitionColumns;
                for (var x = 0; x < DefinitionRows; x++)
                {
                    fullGrid[x,y] = _grid[x,ptr];
                }
            }

            return fullGrid;
        }
    }
}
