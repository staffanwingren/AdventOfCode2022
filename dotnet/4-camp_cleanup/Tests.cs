using NUnit.Framework;

namespace AoC.CampCleanup;

[TestFixture]
public class Tests
{
    private static readonly IEnumerable<string> TestInput = new[]
    {
        "2-4,6-8",
        " 2-3,4-5",
        " 5-7,7-9",
        " 2-8,3-7",
        " 6-6,4-6",
        " 2-6,4-8"
    };

    [Test]
    public void ShouldFindFullyContainedPairs()
    {
        Assert.That(TestInput.NumberOfContainedPairs(), Is.EqualTo(2));
    }

    [Test]
    public void ShouldFindOverlappingPairs()
    {
        Assert.That(TestInput.NumberOfOverlappingPairs(), Is.EqualTo(4));
    }
}
