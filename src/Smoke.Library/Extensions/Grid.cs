#if NET11_0_OR_GREATER
namespace Smoke.Library.Extensions;

public static class Grid
{
    // C# 15: labeled break and continue.
    public static (int Row, int Column)? FindGoal(char[,] grid)
    {
        (int, int)? goal = null;

        outer: for (int row = 0; row < grid.GetLength(0); row++)
        {
            for (int column = 0; column < grid.GetLength(1); column++)
            {
                if (grid[row, column] == '#')
                {
                    continue outer;
                }

                if (grid[row, column] == 'G')
                {
                    goal = (row, column);
                    break outer;
                }
            }
        }

        return goal;
    }

    // C# 15: collection expression arguments.
    public static HashSet<string> UniqueIgnoreCase(params string[] values) =>
        [with(StringComparer.OrdinalIgnoreCase), .. values];

    public static List<string> WithCapacity(string[] values) =>
        [with(capacity: values.Length * 2), .. values];
}
#endif
