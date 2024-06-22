using System;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace GitDemo
{ 
    public class DataDefinition
    {
        private readonly IConfiguration _configuration;
        private const int DefinitionRows = 7;
        private int DefinitionColumns {get; set;}
        private int MaxNumberOfColumns { get; set; }
        private char[,] _grid {get; set;}

        public DataDefinition()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json");

            _configuration = builder.Build();

            DefinitionColumns = int.Parse(_configuration["AppSettings:PartialPatternSize"]);
            MaxNumberOfColumns = int.Parse(_configuration["AppSettings:FullPatternSize"]);

            _grid = new char[DefinitionRows, DefinitionColumns];
        }

        public char[,] ExtractGridFromFile(FileInfo file)
        {
            if (!file.Exists)
            {
                throw new Exception($"Pattern definition does not exist: {file.FullName}");
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
