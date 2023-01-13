// See https://aka.ms/new-console-template for more information
using AoC.Common;

var filename = args.Index(0)!.Or("./input.txt");
var lines = File.ReadLines(filename).ToArray();
var numberOfContainedPairs = lines.NumberOfContainedPairs();
Console.WriteLine($"The number of contained pairs are {numberOfContainedPairs}");
var numberOfOverlappingPairs = lines.NumberOfOverlappingPairs();
Console.WriteLine($"The number of overlapping pairs are {numberOfOverlappingPairs}");

public static class Helpers
{
    public static int NumberOfContainedPairs(this IEnumerable<string> input)
    {
        var containedPairs = 0;
        foreach (var pairs in input.Select(ParseInput))
        {
            if (SeqContainsSeq(pairs.Item1, pairs.Item2) 
                || SeqContainsSeq(pairs.Item2, pairs.Item1))
            {
                containedPairs++;
            }
        }

        return containedPairs;
    }

    public static int NumberOfOverlappingPairs(this IEnumerable<string> input)
    {
        var overlappingPairs = 0;
        foreach (var pair in input.Select(ParseInput))
        {
            if (pair.Item1.SeqOverlapsSeq(pair.Item2) 
                ||  pair.Item2.SeqOverlapsSeq(pair.Item1))
            {
                overlappingPairs++;
            }
        }

        return overlappingPairs;
    }

    public static ((int, int), (int, int)) ParseInput(string line)
        => line
            .Split(',')
            .Select(s => s
                .Split('-')
                .Select(int.Parse)
                .ToTuple())
            .ToTuple();

    public static bool SeqContainsSeq(this (int, int) first, (int, int) second)
        => first.Item1 <= second.Item1 && first.Item2 >= second.Item2;

    public static bool SeqOverlapsSeq(this (int, int) first, (int, int) second)
        => (first.Item1 <= second.Item1 
        && first.Item2 >= second.Item1)
        || (first.Item2 >= second.Item2 
        && first.Item1 <= second.Item2);

    public static (T, T) ToTuple<T>(this IEnumerable<T> seq)
    {
        var arr = seq as T[] ?? seq.ToArray();
        return (arr.First(), arr.Skip(1).First());
    }
}
