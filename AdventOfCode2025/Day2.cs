using System.Text.RegularExpressions;
using Xunit;

namespace AdventOfCode2025
{
    public class Day2
    {
        private const string Version1Regex = "^(?:(\\d)\\1|(\\d{2,})\\2)$";
        private const string Version2Regex = "^(?:(\\d)\\1+|(\\d{2,})\\2+)$";
        private const string TestDataPath = "C:\\Users\\mstat\\source\\repos\\AdventOfCode2025\\Inputs\\day2TestData.csv";

        [Fact]
        public void Part1()
        {
            long invalidSum = SumInvalidProductIds(Version1Regex);

            Assert.Equal(19605500130, invalidSum);
        }

        [Fact]
        public void Part2()
        {
            long invalidSum = SumInvalidProductIds(Version2Regex);

            Assert.Equal(36862281418, invalidSum);
        }

        private long SumInvalidProductIds(string regex)
        {
            long invalidSum = 0;
            Regex rgx = new(regex, RegexOptions.Compiled);
            using var file = new StreamReader(TestDataPath);
            while (file.ReadLine() is { } line)
            {
                var row = line.Split(',');
                foreach (var partialInput in row)
                {
                    var split = partialInput.Split('-');
                    var start = long.Parse(split[0]);
                    var end = long.Parse(split[1]);

                    for (long i = start; i <= end; i++)
                    {
                        if (IsInValid(i, rgx))
                        {
                            invalidSum += i;
                        }
                    }
                }
            }

            return invalidSum;
        }

        private bool IsInValid(long num, Regex rgx)
        {
            string converted = num.ToString();
            return rgx.IsMatch(converted);
        }
    }
}
