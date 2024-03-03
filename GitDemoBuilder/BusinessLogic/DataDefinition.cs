﻿using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace GitDemo
{
    public class DataDefinition
    {
        private const int NumberOfRowsInPattern = 7;
        private readonly int NumberOfColumnsInPattern;
        private readonly char[,] _grid;

        public DataDefinition(FileInfo fileInfo)
        {
            if (!fileInfo.Exists)
            {
                throw new Exception("Pattern definition does not exist");
            }

            string[] rows = File.ReadAllLines(fileInfo.FullName);

            // Check for 7 rows
            if (rows.Length != NumberOfRowsInPattern)
            {
                throw new Exception($"The file must contain exactly {NumberOfRowsInPattern} rows.");
            }

            // Check that all rows are of equal length
            NumberOfColumnsInPattern = rows[0].Length;
            foreach (string row in rows)
            {
                if (row.Length != NumberOfColumnsInPattern)
                {
                    throw new Exception("All rows must be of equal length.");
                }
            }

            // Create a char[,] array from the file contents
            _grid = new char[NumberOfRowsInPattern, NumberOfColumnsInPattern];
            for (int i = 0; i < NumberOfRowsInPattern; i++)
            {
                for (int j = 0; j < NumberOfColumnsInPattern; j++)
                {
                    _grid[i, j] = rows[i][j];
                }
            }
        }

        public char[,] ExtractGridFromFile()
        {
            return _grid;
        }

        public char[,] ExpandExampleGridToFullSize()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            var MaxNumberOfColumns = int.Parse(configuration["AppSettings:FullPatternSize"]);

            var fullGrid = new char[NumberOfRowsInPattern, MaxNumberOfColumns];

            for (var y = 0; y < MaxNumberOfColumns ; y++)
            {
                var ptr = y % NumberOfColumnsInPattern;
                for (var x = 0; x < NumberOfRowsInPattern; x++)
                {
                    fullGrid[x,y] = _grid[x,ptr];
                }
            }

            return fullGrid;
        }
    }
}
