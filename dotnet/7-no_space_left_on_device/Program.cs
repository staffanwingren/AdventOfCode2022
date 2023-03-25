using System.Text.RegularExpressions;
using AoC.Common;

var filename = args.Index(0)!.Or("./input.txt");
var input = File.ReadLines(filename);
var root = input.GenerateTree();
var sizeOfSmallDirs = root.Iter().Where(d => d.Size <= 100000).Sum(d => d.Size);
Console.WriteLine($"Sum of all dirs smaller than 100000 is {sizeOfSmallDirs}");
var dirToDelete = root.FindDirToFreeUpSpace();
Console.WriteLine($"Deleting {dirToDelete?.Name} would free up {dirToDelete?.Size}");

public static class Helpers
{
    public const int DiskSize = 70000000;
    public const int Requirement = 30000000;
    public static readonly Regex NavigationCommand = new Regex("\\$ cd (.+)");
    public static readonly Regex ListCommand = new Regex("\\$ ls");
    public static readonly Regex FileNode = new Regex("(\\d+) (.+)");
    public static readonly Regex DirNode = new Regex("dir (.+)");

    public static DirNode GenerateTree(this IEnumerable<string> input)
    {
        var cd = new DirNode("/");
        var root = cd;
        foreach (var line in input)
        {
            var navigation = NavigationCommand.Match(line);
            if (navigation.Success)
            {
                var nav = navigation.Groups[1];
                if (nav.Value.Equals(".."))
                {
                    cd = cd!.Parent;
                }
                else if (nav.Value.StartsWith('/'))
                {
                    cd = root;
                }
                else
                {
                    cd = cd!.GetChild(nav.Value);
                }
            }

            var list = ListCommand.Match(line);
            if (list.Success)
            {
                continue;
            }

            var file = FileNode.Match(line);
            if (file.Success)
            {
                var size = int.Parse(file.Groups[1].Value);
                var fileName = file.Groups[2].Value;
                cd!.AddFile(new FileNode(size, fileName, cd));
            }

            var dir = DirNode.Match(line);
            if (dir.Success)
            {
                var dirName = dir.Groups[1].Value;
                cd!.AddDir(new DirNode(dirName, cd));
            }
        }

        return root;
    }

    public static DirNode? FindDirToFreeUpSpace(this DirNode root, int requirement = Requirement, int diskSpace = DiskSize)
    {
        var delta = diskSpace - root.Size - requirement;
        if (int.IsPositive(delta))
        {
            return null;
        }

        return root.Iter().Where(d => d.Size > Math.Abs(delta)).MinBy(d => d.Size);
    }
}

public enum NodeType
{
    File, 
    Dir
}

public interface INode
{
    int Size { get; }
    string Name { get; }
    NodeType Type { get; }
    DirNode? Parent { get; }
}

public class DirNode : INode
{
    private List<DirNode> _childDirs;
    private List<FileNode> _childFiles;
    public int Size => _childDirs.Sum(d => d.Size) + _childFiles.Sum(f => f.Size);

    public string Name { get; }
    public NodeType Type => NodeType.Dir;
    public DirNode? Parent { get; }

    public DirNode(string name, DirNode? parent = null)
    {
        _childDirs = new List<DirNode>();
        _childFiles = new List<FileNode>();
        Name = name;
        Parent = parent;
    }

    public DirNode GetChild(string name)
    {
        return _childDirs.First(d => d.Name == name);
    }

    public void AddFile(FileNode fileNode)
    {
        _childFiles.Add(fileNode);
    }

    public void AddDir(DirNode dirNode)
    {
        _childDirs.Add(dirNode);
    }

    public IEnumerable<DirNode> Iter()
    {
        yield return this;
        foreach (var dir in _childDirs)
        {
            foreach (var child in dir.Iter())
            {
                yield return child;
            }
        }
    }
}

public class FileNode : INode
{
    public int Size { get; }
    public string Name { get; }
    public NodeType Type => NodeType.File;
    public DirNode Parent { get; }

    public FileNode(int size, string name, DirNode parent)
    {
        Size = size;
        Name = name;
        Parent = parent;
    }
}
