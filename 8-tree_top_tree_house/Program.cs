// See https://aka.ms/new-console-template for more information
var input = File.ReadLines(args[0]);
var forest = input.ToForest();
Console.WriteLine($"There are {forest.CountVisibleTrees()} number of visible trees in the forest.");
Console.WriteLine($"The highest scenic score is {forest.FindHighestScenicScore()}.");

public static class Helper
{
    public static char[][] ToForest(this IEnumerable<string> input)
    {
        return input.Select(s => s.ToCharArray()).ToArray();
    }
    
    public static int CountVisibleTrees(this char[][] forest)
    {
        var rows = forest.Length;
        var cols = forest[0].Length;
        var visibleTrees = rows * 2 + (cols - 2) * 2;
        for (var r = 1; r < rows - 1; r++)
        {
            for (var c = 1; c < cols - 1; c++)
            {
                if (IsVisible(forest, r, c))
                {
                    visibleTrees++;
                }
            }
        }

        return visibleTrees;
    }
    
    public static int FindHighestScenicScore(this char[][] forest)
    {
        var rows = forest.Length;
        var cols = forest[0].Length;
        var maxScenicScore = 0;
        for (var r = 1; r < rows - 1; r++)
        {
            for (var c = 1; c < cols - 1; c++)
            {
                var score = ScenicScore(forest, r, c);
                maxScenicScore = Math.Max(maxScenicScore, score);
            }
        }

        return maxScenicScore;
    }
    
    public static bool IsVisible(this char[][] forest, int rowIndex, int colIndex)
    {
        var row = forest[rowIndex];
        var column = forest.Select(r => r[colIndex]).ToArray();
        var targetHeight = row[colIndex];
        var peaks = new List<int>();
        peaks.Add(row[..colIndex].Max());
        peaks.Add(row[(colIndex+1)..].Max());
        peaks.Add(column[..rowIndex].Max());
        peaks.Add(column[(rowIndex+1)..].Max());
        return peaks.Min() < targetHeight;
    }

    public static int ScenicScore(this char[][] forest, int rowIndex, int colIndex)
    {
        var up = 0;
        var down = 0;
        var left = 0;
        var right = 0;
        var targetHeight = forest[rowIndex][colIndex];
        for (var r = rowIndex-1; r >= 0; r--)
        {
            left++;
            if (forest[r][colIndex] >= targetHeight)
            {
                break;
            }
        }

        for (var r = rowIndex + 1; r < forest.Length; r++)
        {
            right++;
            if (forest[r][colIndex] >= targetHeight)
            {
                break;
            }
        }

        for (var c = colIndex - 1; c >= 0; c--)
        {
            up++;
            if (forest[rowIndex][c] >= targetHeight)
            {
                break;
            }
        }

        for (var c = colIndex + 1; c < forest[0].Length; c++)
        {
            down++;
            if (forest[rowIndex][c] >= targetHeight)
            {
                break;
            }
        }

        return up * down * left * right;
    }
}