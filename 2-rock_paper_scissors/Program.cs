// See https://aka.ms/new-console-template for more information
using AoC.Common;

var total1 = 0;
var total2 = 0;
var filename = args.Index(0)!.Or("./input.txt");
foreach (var line in File.ReadLines(filename))
{
    var inputChars = line.Split(' ');
    var opponentHand = inputChars[0].ToHand();
    var ownHand1 = inputChars[1].ToHand();
    var ownHand2 = inputChars[1].ToHand(opponentHand);
    var points1 = ownHand1.PointAgainst(opponentHand);
    var points2 = ownHand2.PointAgainst(opponentHand);
    total1 += points1;
    total2 += points2;
    Console.WriteLine($"{opponentHand}, {ownHand1}: {points1}/{points2}");
}

Console.WriteLine($"Results calculated: {total1}/{total2} points");

public enum Hand
{
    Rock = 1,
    Paper = 2,
    Scissors = 3
}

public enum Outcome
{
    Loose = 0,
    Draw = 3,
    Win = 6
}

static class HandExtentions
{
    public static Hand ToHand(this string hand)
        => hand switch
        {
            "A" => Hand.Rock,
            "B" => Hand.Paper,
            "C" => Hand.Scissors,
            "X" => Hand.Rock, 
            "Y" => Hand.Paper, 
            "Z" => Hand.Scissors,
            _ => throw new ArgumentException()
        };

    public static Hand ToHand(this string hand, Hand opponentHand) =>
        hand switch
        {
            "X" => opponentHand switch
            {
                Hand.Rock => Hand.Scissors,
                Hand.Paper => Hand.Rock,
                Hand.Scissors => Hand.Paper
            },
            "Y" => opponentHand,
            "Z" => opponentHand switch
            {
                Hand.Rock => Hand.Paper,
                Hand.Paper => Hand.Scissors,
                Hand.Scissors => Hand.Rock
            },
            _ => throw new ArgumentException()
        };

    public static int PointAgainst(this Hand ownHand, Hand opponentHand)
    {
        return (int)ownHand + (int)ownHand.OutcomeAgainst(opponentHand);
    }

    public static Outcome OutcomeAgainst(this Hand ownHand, Hand opponentHand)
    {
        if (ownHand == opponentHand) return Outcome.Draw;
        if (ownHand == Hand.Scissors && opponentHand == Hand.Rock) return Outcome.Loose;
        if (ownHand == Hand.Rock && opponentHand == Hand.Scissors) return Outcome.Win;
        if ((int)ownHand > (int)opponentHand) return Outcome.Win;
        return Outcome.Loose;
    }
}
