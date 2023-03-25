using AoC.Common;

var filename = args.Index(0)!.Or("./input.txt");
var input = File.ReadAllText(filename);
Console.WriteLine($"The start-of-packet is a position {input.FindStartOfPacket()}");
Console.WriteLine($"The start-of-message is a position {input.FindStartOfMessage()}");

public static class Helper
{
    public static int FindStartOfPacket(this string data)
        => FindDistinct(data, 4);

    public static int FindStartOfMessage(this string data)
        => FindDistinct(data, 14);

    private static int FindDistinct(string data, int seqLength)
    {
        var queue = new Queue<char>(seqLength);
        for (int i = 0; i < data.Length; i++)
        {
            while (queue.Count >= seqLength)
            {
                queue.Dequeue();
            }

            queue.Enqueue(data[i]);
            if (queue.Distinct().Count() == seqLength)
            {
                return ++i;
            }
        }

        return -1;
    }
}
