using System.Collections.Generic;
using NodaTime;

namespace GitDemo
{
    public class DateComposer
    {
        private readonly List<Instant> _patternDates = new List<Instant>();
        private Instant StartDate { get; }

        public DateComposer()
        {
            var now = SystemClock.Instance.GetCurrentInstant();
            var duration = Duration.FromDays(365);
            StartDate = now.Plus(-duration);
        }

        public DateComposer(Instant now)
        {
            var duration = Duration.FromDays(365);
            StartDate = now.Plus(-duration);
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
