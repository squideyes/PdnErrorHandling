// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace BasicsDemo;

public static class Safe
{
    public static bool Do<T>(this T value, Action<T> action)
    {
        try
        {
            action(value);

            return true;
        }
        catch
        {
            return false;
        }
    }

    public static R Convert<T, R>(this T value, Func<T, R> convert)
    {
        if (!TryConvert(value, convert, out R converted))
            return converted;
        else
            return default!;
    }

    public static bool TryConvert<T, R>(
        this T value, Func<T, R> convert, out R converted)
    {
        try
        {
            converted = convert(value);

            return true;
        }
        catch
        {
            converted = default!;

            return false;
        }
    }
}