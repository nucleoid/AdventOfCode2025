using Xunit;

namespace AdventOfCode2025
{
    public class Day3
    {
        private const string TestDataPath = "C:\\Users\\mstat\\source\\repos\\AdventOfCode2025\\inputDay3.txt";

        [Fact]
        public void Part1()
        {
            var joltage = CalculateJoltage();

            Assert.Equal(357, joltage);
        }

        [Fact]
        public void Part2()
        {
            var joltage = CalculateJoltage2();

            Assert.Equal(joltage, 172366781546707L);
            Assert.True(joltage > 172366781546707L);
        }

        private int CalculateJoltage()
        {
            using var file = new StreamReader(TestDataPath);

            var bankJoltageSum = 0;

            while (file.ReadLine() is { } line)
            {
                var numbers = line.ToCharArray().Select(x => int.Parse(x.ToString())).ToList();
                var max = numbers.Take(numbers.Count - 1).Max();
                var maxIndex = numbers.IndexOf(max);
                var nextMax = numbers[(maxIndex+1)..].Max();
                bankJoltageSum += int.Parse(max.ToString() + nextMax.ToString());
            }

            return bankJoltageSum;
        }

        private long CalculateJoltage2()
        {
            using var file = new StreamReader(TestDataPath);
            var bankJoltageSum = 0L;

            while (file.ReadLine() is { } line)
            {
                var numbers = line.ToCharArray().Select(x => int.Parse(x.ToString())).ToList();
                var max = numbers[..89].Max();
                var maxIndex0 = numbers.IndexOf(max);

                var (nextMax, maxIndex1) = NextMax(numbers, maxIndex0, 90);
                var (nextMax2, maxIndex2) = NextMax(numbers, maxIndex1, 91);
                var (nextMax3, maxIndex3) = NextMax(numbers, maxIndex2, 92);
                var (nextMax4, maxIndex4) = NextMax(numbers, maxIndex3, 93);
                var (nextMax5, maxIndex5) = NextMax(numbers, maxIndex4, 94);
                var (nextMax6, maxIndex6) = NextMax(numbers, maxIndex5, 95);
                var (nextMax7, maxIndex7) = NextMax(numbers, maxIndex6, 96);
                var (nextMax8, maxIndex8) = NextMax(numbers, maxIndex7, 97);
                var (nextMax9, maxIndex9) = NextMax(numbers, maxIndex8, 98);
                var (nextMax10, maxIndex10) = NextMax(numbers, maxIndex9, 99);
                var (nextMax11, _) = NextMax(numbers, maxIndex10, 100);

                var bankVoltage = long.Parse(max.ToString() + nextMax.ToString() + nextMax2.ToString() + nextMax3.ToString() + nextMax4.ToString() +
                                              nextMax5.ToString() + nextMax6.ToString() + nextMax7.ToString() + nextMax8.ToString() + nextMax9.ToString() +
                                              nextMax10.ToString() + nextMax11.ToString());

                bankJoltageSum += bankVoltage;
            }

            (int, int) NextMax(List<int> ints, int maxIndex, int nextLow)
            {
                var nextMax = ints[(maxIndex + 1)..nextLow].Max();
                var nextMaxIndex = ints[(maxIndex + 1)..nextLow].IndexOf(nextMax) + maxIndex + 1;
                return (nextMax, nextMaxIndex);
            }

            return bankJoltageSum;
        }



    }
}
