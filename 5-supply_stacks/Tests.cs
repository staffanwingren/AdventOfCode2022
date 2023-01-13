using NUnit.Framework;

namespace Aoc.SupplyStacks;

[TestFixture]
public class Tests 
{
    public readonly IEnumerable<string> TestInput = new[] 
    {
        "    [D]    ",
        "[N] [C]    ",
        "[Z] [M] [P]",
        " 1   2   3 ",
        "",
        "move 1 from 2 to 1",
        "move 3 from 1 to 3",
        "move 2 from 2 to 1",
        "move 1 from 1 to 2"
    };

    [Test]
    public void ShouldExtractStacksSection()
    {
        Assert.That(TestInput.ExtractStacksSection(), Is.EqualTo(TestInput.Take(4)));
    }
    
    [Test]
    public void ShouldParseStacks() 
    {
        var stacks = TestInput.ExtractStacksSection().ParseStacks();
        Assert.That(stacks["1"].Peek(), Is.EqualTo('N'));
        Assert.That(stacks["1"].Count, Is.EqualTo(2));
        Assert.That(stacks["3"].Peek(), Is.EqualTo('P'));
        Assert.That(stacks["3"].Count, Is.EqualTo(1));
    }
    
    [Test]
    public void ShouldExtractInstructionsSection() 
    {
        Assert.That(TestInput.ExtractInstructionsSection(), Is.EqualTo(TestInput.Skip(5)));
    }
    
    [Test]
    public void ShouldParseInstructions()
    {
        var instructions = TestInput.ExtractInstructionsSection().ParseInstructions();
        Assert.That(instructions.First().Repetitions, Is.EqualTo(1));
        Assert.That(instructions.First().FromStack, Is.EqualTo("2"));
        Assert.That(instructions.First().ToStack, Is.EqualTo("1"));
        Assert.That(instructions.Skip(1).First().Repetitions, Is.EqualTo(3));
        Assert.That(instructions.Skip(1).First().FromStack, Is.EqualTo("1"));
        Assert.That(instructions.Skip(1).First().ToStack, Is.EqualTo("3"));
        Assert.That(instructions.Count, Is.EqualTo(4));
    }
    
    [Test]
    public void ShouldApplyCrateMover9000()
    {
        var stacks = TestInput.ExtractStacksSection().ParseStacks();
        var instructions = TestInput.ExtractInstructionsSection().ParseInstructions();
        stacks.ApplyCrateMover9000(instructions);
        Assert.That(stacks["1"].Peek(), Is.EqualTo('C'));
        Assert.That(stacks["1"].Count, Is.EqualTo(1));
        Assert.That(stacks["3"].Peek(), Is.EqualTo('Z'));
        Assert.That(stacks["3"].Count, Is.EqualTo(4));
    }
    
    [Test]
    public void ShouldApplyCrateMover9001()
    {
        var stacks = TestInput.ExtractStacksSection().ParseStacks();
        var instructions = TestInput.ExtractInstructionsSection().ParseInstructions();
        stacks.ApplyCrateMover9001(instructions);
        Assert.That(stacks["1"].Peek(), Is.EqualTo('M'));
        Assert.That(stacks["1"].Count, Is.EqualTo(1));
        Assert.That(stacks["3"].Peek(), Is.EqualTo('D'));
        Assert.That(stacks["3"].Count, Is.EqualTo(4));
    }
    
    [Test]
    public void ShouldReadTopOfStacks()
    {
        Assert.That(TestInput.ExtractStacksSection().ParseStacks().ViewTop(), Is.EqualTo("NDP"));
    }
}
