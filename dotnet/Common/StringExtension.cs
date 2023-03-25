namespace AoC.Common;

public static class StringExtensions {
    public static string Or(this string input, string replacement) {
        return string.IsNullOrWhiteSpace(input) ? replacement : input;
    }

    public static string? Index(this string[] args, int index) {
        return args.Length > index ? args[index] : null;
    }
}
