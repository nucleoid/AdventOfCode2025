
namespace AdventOfCode2025.Utils
{
    public static class GridUtility
    {
        public static (char[,], int, int) LoadGrid(string input)
        {
            string[] lines = File.ReadAllLines(input);

            char[,] grid = new char[lines.Length, lines[0].Length];
            var rows = lines.Length;
            var cols = lines[0].Length;

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    grid[row, col] = lines[row][col];
                }
            }

            return (grid, rows, cols);
        }
    }
}
