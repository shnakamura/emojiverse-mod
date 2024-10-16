using System.Runtime.CompilerServices;

namespace Emojiverse.Utilities;

public static class StringExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string SurroundWith(this string value, char character) {
        return $"{character}{value}{character}";
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string SurroundWith(this string value, string character) {
        return $"{character}{value}{character}";
    }
}
