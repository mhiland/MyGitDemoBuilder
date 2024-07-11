using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using NodaTime;
using NodaTime.Text;

namespace GitDemo
{
    public class DateComposer
    {
        private readonly List<Instant> _patternDates = new List<Instant>();
        private Instant StartDate { get; }
        private static IConfiguration _configuration;

        public DateComposer()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json");

            _configuration = builder.Build();

            var startDateValue = _configuration["AppSettings:StartDate"];

            StartDate = ConvertToInstant(startDateValue);
        }

        private static Instant ConvertToInstant(string dateTimeString)
        {
            var pattern = InstantPattern.ExtendedIso;

            var parseResult = pattern.Parse(dateTimeString);

            if (parseResult.Success)
            {
                return parseResult.Value;
            }
            else
            {
                throw new FormatException($"Invalid date-time string: {dateTimeString}");
            }
        }

        public List<Instant> GetDateFromPattern(char[,] pattern)
        {
            for (var i = 0; i < pattern.GetLength(0); i++)
            {
                for (var j = 0; j < pattern.GetLength(1); j++)
                {
                    EvaluatePatternCharacter(pattern, i, j);
                }
            }

            return _patternDates;
        }

        private void EvaluatePatternCharacter(char[,] pattern, int i, int j)
        {
            var character = pattern[i, j];
            var intValue = int.Parse(character.ToString());

            if (intValue == 0) return;

            for (var intensity = 0; intensity < intValue; intensity++)
            {
                var offsetInDays = j * 7 + i;
                var instant = StartDate.Plus(Duration.FromDays(offsetInDays).Plus(Duration.FromMilliseconds(intensity)));
                _patternDates.Add(instant);
            }
        }
    }
}
