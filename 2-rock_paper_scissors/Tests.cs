using NUnit.Framework;

namespace _2_rock_paper_scissors;

[TestFixture]
public class Tests
{
    [TestCase(Hand.Rock, Hand.Rock, Outcome.Draw)]
    [TestCase(Hand.Paper, Hand.Paper, Outcome.Draw)]
    [TestCase(Hand.Scissors, Hand.Scissors, Outcome.Draw)]
    [TestCase(Hand.Rock, Hand.Paper, Outcome.Loose)]
    [TestCase(Hand.Paper, Hand.Scissors, Outcome.Loose)]
    [TestCase(Hand.Scissors, Hand.Rock, Outcome.Loose)]
    [TestCase(Hand.Rock, Hand.Scissors, Outcome.Win)]
    [TestCase(Hand.Paper, Hand.Rock, Outcome.Win)]
    [TestCase(Hand.Scissors, Hand.Paper, Outcome.Win)]
    public void TestOutcome(Hand ownHand, Hand opponentHand, Outcome outcome)
    {
        Assert.That(ownHand.OutcomeAgainst(opponentHand), Is.EqualTo(outcome));
    }
    
    [TestCase("X", Hand.Rock, Hand.Scissors)]
    [TestCase("X", Hand.Paper, Hand.Rock)]
    [TestCase("X", Hand.Scissors, Hand.Paper)]
    [TestCase("Y", Hand.Rock, Hand.Rock)]
    [TestCase("Y", Hand.Paper, Hand.Paper)]
    [TestCase("Y", Hand.Scissors, Hand.Scissors)]
    [TestCase("Z", Hand.Rock, Hand.Paper)]
    [TestCase("Z", Hand.Paper, Hand.Scissors)]
    [TestCase("Z", Hand.Scissors, Hand.Rock)]
    public void TestToHand2(string ownHand, Hand opponentHand, Hand hand)
    {
        Assert.That(ownHand.ToHand(opponentHand), Is.EqualTo(hand));
    }
}