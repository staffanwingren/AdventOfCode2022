// See https://aka.ms/new-console-template for more information
using AoC.Common;

var filename = args.Index(0)!.Or("input.txt");
var current = 0;
var currentElf = 1;
var highScore = new Dictionary<int, int>();
foreach (var line in File.ReadLines(filename))
{
    if (string.IsNullOrWhiteSpace(line))
    {
        highScore.Add(currentElf, current);
        currentElf++;
        current = 0;
    }
    else
    {
        var calories = int.Parse(line);
        current += calories;
    }
}
highScore.Add(currentElf, current);

Console.WriteLine($"Computation done! There where {highScore.Count} elves carrying {highScore.Values.Sum()} calories");
var high = highScore.OrderByDescending(kv => kv.Value).First();
Console.WriteLine($"Elf number {high.Value} has the most calories ({high.Key})");

var highThree = highScore.OrderByDescending(kv => kv.Value).Take(3).Sum(kv => kv.Value);
Console.WriteLine($"The top 3 elves carries {highThree} calories");
