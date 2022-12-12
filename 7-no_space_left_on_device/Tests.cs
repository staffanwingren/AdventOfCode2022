using NUnit.Framework;

namespace _7_no_space_left_on_device;

[TestFixture]
public class Tests
{
    private readonly string[] Input = new[]
    {
        "$ cd /",
        "$ ls",
        "dir a",
        "14848514 b.txt",
        "8504156 c.dat",
        "dir d",
        "$ cd a",
        "$ ls",
        "dir e",
        "29116 f",
        "2557 g",
        "62596 h.lst",
        "$ cd e",
        "$ ls",
        "584 i",
        "$ cd ..",
        "$ cd ..",
        "$ cd d",
        "$ ls",
        "4060174 j",
        "8033020 d.log",
        "5626152 d.ext",
        "7214296 k ",
    };

    [Test]
    public void ShouldCalculateSizeOfRoot()
    {
        var tree = Input.GenerateTree();
        Assert.That(tree.Size, Is.EqualTo(48381165));
    }

    [Test]
    public void ShouldCalculateSizeOfA()
    {
        var tree = Input.GenerateTree();
        var a = tree.GetChild("a");
        Assert.That(a.Size, Is.EqualTo(94853));
    }

    [Test]
    public void ShouldCalculateSizeOfD()
    {
        var tree = Input.GenerateTree();
        var d = tree.GetChild("d");
        Assert.That(d.Size, Is.EqualTo(24933642));
    }

    [Test]
    public void ShouldCalculateSumOfDirsLessThan100000()
    {
        var dirs = Input.GenerateTree().Iter().Where(d => d.Size <= 100000).Select(d => d.Name);
        Assert.That(dirs, Contains.Item("a"));
        Assert.That(dirs, Contains.Item("e"));
        Assert.That(dirs.Count(), Is.EqualTo(2));
    }

    [Test]
    public void ShouldReturnTheSmallestDirNeeded()
    {
        var dir = Input.GenerateTree().FindDirToFreeUpSpace();
        Assert.That(dir.Name, Is.EqualTo("d"));
        Assert.That(dir.Size, Is.EqualTo(24933642));
    }
}