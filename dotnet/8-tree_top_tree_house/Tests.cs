using NUnit.Framework;

namespace AoC.TreeTopTreeHouse;

[TestFixture]
public class Tests
{
    public static readonly string[] TestInput = new[]
    {
        "30373",
        "25512",
        "65332",
        "33549",
        "35390"
    };

    [TestCase(1, 1, true)]
    [TestCase(1, 2, true)]
    [TestCase(1, 3, false)]
    [TestCase(2, 1, true)]
    [TestCase(2, 2, false)]
    [TestCase(2, 3, true)]
    [TestCase(3, 1, false)]
    [TestCase(3, 2, true)]
    [TestCase(3, 3, false)]
    public void ShouldReturnVisibility(int rowIndex, int colIndex, bool expected)
    {
        var result = TestInput.ToForest().IsVisible(rowIndex, colIndex);
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ShouldCountVisibleTrees()
    {
        var result = TestInput.ToForest().CountVisibleTrees();
        Assert.That(result, Is.EqualTo(21));
    }

    [TestCase(1, 2, 4)]
    [TestCase(3, 2, 8)]
    public void ShouldCalculateScenicScore(int rowIndex, int colIndex, int expected)
    {
        var result = TestInput.ToForest().ScenicScore(rowIndex, colIndex);
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void ShouldFindMaxScenicScore()
    {
        var result = TestInput.ToForest().FindHighestScenicScore();
        Assert.That(result, Is.EqualTo(8));
    }
}
