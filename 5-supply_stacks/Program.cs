// See https://aka.ms/new-console-template for more information
using AoC.Common;

var filename = args.Index(0)!.Or("./input.txt");
var input = File.ReadLines(filename);
var stacks = input.ExtractStacksSection().ParseStacks();
var instructions = input.ExtractInstructionsSection().ParseInstructions();
Console.WriteLine($"Top view before CrateMover9000 is '{stacks.ViewTop()}'");
stacks.ApplyCrateMover9000(instructions);
Console.WriteLine($"Top view after CrateMover9000 is '{stacks.ViewTop()}'");
stacks = input.ExtractStacksSection().ParseStacks();
instructions = input.ExtractInstructionsSection().ParseInstructions();
Console.WriteLine($"Top view before CrateMover9001 is '{stacks.ViewTop()}'");
stacks.ApplyCrateMover9001(instructions);
Console.WriteLine($"Top view after CrateMover9001 is '{stacks.ViewTop()}'");

public static class Helpers
{
	public static IEnumerable<string> ExtractStacksSection(this IEnumerable<string> input)
		=> input.TakeWhile(l => !string.IsNullOrWhiteSpace(l));
		
	public static Dictionary<string, Stack<char>> ParseStacks(this IEnumerable<string> stackSection)
	{
		var arr = stackSection.ToArray();
		var names = arr.Last().Split(' ', StringSplitOptions.RemoveEmptyEntries);
		var numberOfStacks = names.Length;
		var stacks = new Dictionary<string, Stack<char>>();
		for (var i = arr.Length - 2; i >= 0; i--)
		{
			var crates = arr[i].Chunk(4).Select(s => s.Skip(1).First()).ToArray();
			for (var stack = 0; stack < names.Length; stack++)
			{
				if (string.IsNullOrWhiteSpace(crates[stack].ToString()))
				{
					continue;
				}
				
				if (!stacks.ContainsKey(names[stack])) 
				{
					stacks.Add(names[stack], new Stack<char>());
				}
				
				stacks[names[stack]].Push(crates[stack]);
			}
		}
		
		return stacks;
	}
	
	public static IEnumerable<string> ExtractInstructionsSection(this IEnumerable<string> input)
		=> input.SkipWhile(l => !string.IsNullOrWhiteSpace(l)).Skip(1);
	
	public static IEnumerable<Instruction> ParseInstructions(this IEnumerable<string> instructions)
	{
		foreach (var instruction in instructions)
		{
			var terms = instruction.Split(' ').ToArray();
			yield return new Instruction(int.Parse(terms[1]), terms[3], terms[5]);
		}
	}
	
	public static void ApplyCrateMover9000(this Dictionary<string, Stack<char>> stacks, IEnumerable<Instruction> instructions)
	{
		foreach(var instruction in instructions)
		{
			for (var i = instruction.Repetitions; i > 0; i--)
			{
				var crate = stacks[instruction.FromStack].Pop();
				stacks[instruction.ToStack].Push(crate);
			}
		}
	}
	
	public static void ApplyCrateMover9001(this Dictionary<string, Stack<char>> stacks, IEnumerable<Instruction> instructions)
	{
		foreach(var instruction in instructions)
		{
			var moverStack = new Stack<char>();
			for (var i = instruction.Repetitions; i > 0; i--)
			{
				var crate = stacks[instruction.FromStack].Pop();
				moverStack.Push(crate);
			}
			
			while(moverStack.TryPeek(out _))
			{
				stacks[instruction.ToStack].Push(moverStack.Pop());
			}
		}
	}
	
	public static string ViewTop(this Dictionary<string, Stack<char>> stacks)
		=> new String(stacks.OrderBy(s => int.Parse(s.Key)).Select(s => s.Value.Peek()).ToArray());
}

public class Instruction 
{
	public int Repetitions { get; }
	public string FromStack { get; }
	public string ToStack { get; }
	
	public Instruction(int repetitions, string from, string to)
	{
		Repetitions = repetitions;
		FromStack = from;
		ToStack = to;
	}
}
