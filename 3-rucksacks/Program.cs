// See https://aka.ms/new-console-template for more information
using AoC.Common;

var filename = args.Index(0)!.Or("./input.txt");
var input = File.ReadLines(filename).ToArray();
var rucksackPriority = 0;
var groupPriority = 0;

for (var i = 0; i < input.Length; i++)
{
    var middle = input[i].Length / 2;
    var compartmentA = input[i][..middle];
    var compartmentB = input[i][middle..];
    foreach (var c in compartmentA.Where(c => compartmentB.Contains(c)))
    {
        rucksackPriority += CalculatePriority(c);
        break;
    }

    if ((i + 1) % 3 == 0)
    {
        foreach (var c in input[i].Where(c => input[i - 2].Contains(c) && input[i - 1].Contains(c)))
        {
            groupPriority += CalculatePriority(c);
            break;
        }
    }
}

int CalculatePriority(char c) =>
    c is >= 'a' and <= 'z' ? c - 'a' + 1 : c - 'A' + 27;

Console.WriteLine($"The sum of rucksack priorities is {rucksackPriority}");
Console.WriteLine($"The sum of group priorities is {groupPriority}");
