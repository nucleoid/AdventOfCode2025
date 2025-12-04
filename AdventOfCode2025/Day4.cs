using AdventOfCode2025.Utils;
using Xunit;

namespace AdventOfCode2025
{
    public class Day4
    {
        [Theory]
        [InlineData(TestInput.ExampleTestDataPathDay4, 13)]
        [InlineData(TestInput.TestDataPathDay4, 1480)]
        public void Part1(string input, long expected)
        {
            var (grid, rows, cols) = GridUtility.LoadGrid(input);
            var (accessible, _) = Accessible(grid, rows, cols, '@');

            Assert.Equal(expected, accessible);
        }

        [Theory]
        [InlineData(TestInput.ExampleTestDataPathDay4, 43)]
        [InlineData(TestInput.TestDataPathDay4, 8899)]
        public void Part2(string input, long expected)
        {
            var (grid, rows, cols) = GridUtility.LoadGrid(input);

            int total = 0;
            int accessible;
            do
            {
                (accessible, grid) = Accessible(grid, rows, cols, '@', 2);
                total += accessible;
            } while (accessible > 0);


            Assert.Equal(expected, total);
        }

        private static (int, char[,] grid) Accessible(char[,] grid, int rows, int cols, char trigger, int version = 1)
        {
            var accessible = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    var character = grid[i, j];
                    if (character == trigger)
                    {
                        var adjacentList = new List<char>
                        {
                            CheckIndex(i - 1, j - 1, grid, rows, cols), //topLeft (up one row, left one column)
                            CheckIndex(i - 1, j, grid, rows, cols),     //topCenter (up one row, same column)
                            CheckIndex(i - 1, j + 1, grid, rows, cols), //topRight (up one row, right one column)
                            CheckIndex(i, j - 1, grid, rows, cols),     //middleLeft (same row, left one column)
                            CheckIndex(i, j + 1, grid, rows, cols),     //middleRight (same row, right one column)
                            CheckIndex(i + 1, j - 1, grid, rows, cols), //bottomLeft (down one row, left one column)
                            CheckIndex(i + 1, j, grid, rows, cols),     //bottomCenter (down one row, same column)
                            CheckIndex(i + 1, j + 1, grid, rows, cols), //bottomRight (down one row, right one column)
                        };

                        if (adjacentList.Count(x => x == trigger) < 4)
                        {
                            accessible++;
                            if (version == 2) grid[i, j] = '.';
                        }
                    }
                }
            }

            return (accessible, grid);
        }

        private static char CheckIndex(int row, int col, char[,] grid, int rows, int cols)
        {
            return row >= 0 && row < rows && col >= 0 && col < cols ? grid[row, col] : '.';
        }
    }
}
