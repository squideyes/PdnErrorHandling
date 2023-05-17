// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace ValidationDemo;

public static class MiscExtenders
{
    private static readonly HashSet<char> base32Chars = 
        "ABCDEFGHJKLMNPQRSTUVWXYZ23456789".ToHashSet();

    public static bool IsBase32(this string value) =>
        value.All(base32Chars.Contains);

    public static bool IsNonEmptyAndTrimmed(this string value)
    {
        return !string.IsNullOrEmpty(value)
            && !char.IsWhiteSpace(value[0])
            && !char.IsWhiteSpace(value[^1]);
    }
}