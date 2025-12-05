using AdventOfCode2025.Utils;
using Xunit;

namespace AdventOfCode2025
{
    public class Day5
    {
        [Theory]
        [InlineData(TestInput.ExampleTestDataPathDay5, 3)]
        [InlineData(TestInput.TestDataPathDay5, 701)]
        public void Part1(string input, long expected)
        {
            var freshIds = new List<NewRecord>();
            long count = 0;
            bool startUp = false;
            string[] lines = File.ReadAllLines(input);
            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                if (string.IsNullOrEmpty(line))
                {
                    startUp = true;
                    continue;
                }
                if (!startUp)
                {
                    var split = line.Split('-');
                    var start = long.Parse(split[0]);
                    var end = long.Parse(split[1]);

                    freshIds.Add(new NewRecord(start, end));
                }
                else
                {
                    var idToCheck = long.Parse(line);
                    if (freshIds.Any(x => idToCheck >= x.Start && idToCheck <= x.End ))
                    {
                        count++;
                    }
                }
            }

            Assert.Equal(expected, count);
        }

        [Theory]
        [InlineData(TestInput.ExampleTestDataPathDay5, 14)]
        [InlineData(TestInput.TestDataPathDay5, 352340558684863)]
        public void Part2(string input, long expected)
        {
            var freshIds = new List<NewRecord>();
            string[] lines = File.ReadAllLines(input);
            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                if (string.IsNullOrEmpty(line))
                {
                    break;
                }
                var split = line.Split('-');
                var start = long.Parse(split[0]);
                var end = long.Parse(split[1]);

                freshIds.Add(new NewRecord(start, end));
            }

            var sortedIds = freshIds.OrderBy(x => x.Start).ToList();
            var mergedRanges = new List<NewRecord>();
            
            foreach (var record in sortedIds)
            {
                if (mergedRanges.Count == 0 || mergedRanges.Last().End < record.Start - 1)
                {
                    mergedRanges.Add(record);
                }
                else
                {
                    var last = mergedRanges.Last();

                    //mergedRanges[mergedRanges.Count - 1] = new NewRecord(last.Start, Math.Max(last.End, record.End));
                    mergedRanges[^1] = last with { End = Math.Max(last.End, record.End) };
                }
            }

            long count = mergedRanges.Sum(x => x.End - x.Start + 1);

            Assert.Equal(expected, count);
        }
    }

    internal record NewRecord(long Start, long End);
}
