// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Runtime.CompilerServices;

namespace SquidEyes.PdnLogSmart.LogSmart;

public static class ValidationExtenders
{
    public static T ThrowIfInvalid<T>(this T argument, Func<T, bool> isValid,
        [CallerArgumentExpression(nameof(argument))] string? argName = null)
    {
        if (isValid is not null && !isValid(argument))
            Must(argName!, $"a valid {typeof(T)} value");

        return argument;
    }

    public static T ThrowIfDefault<T>(this T argument,
        [CallerArgumentExpression(nameof(argument))] string? argName = null)
    {
        if (argument.IsDefault())
            MayNot(argName!, $"a default \"{typeof(T)}\"");

        return argument;
    }

    public static string  ThrowIfNullOrEmpty(this string argument,
        [CallerArgumentExpression(nameof(argument))] string? argName = null)
    {
        if (string.IsNullOrEmpty(argument))
            MayNot(argName!, "null or empty");


        return argument;
    }

    public static string ThrowIfNullEmptyOrWhitespace(this string argument,
        [CallerArgumentExpression(nameof(argument))] string? argName = null)
    {
        if (string.IsNullOrWhiteSpace(argument))
            MayNot(argName!, "null, empty or whitespace");

        return argument;
    }

    public static T ThrowIfNotDefined<T>(this T argument,
        [CallerArgumentExpression(nameof(argument))] string? argName = null)
        where T : struct, Enum
    {
        if (!Enum.IsDefined(argument))
            Must(argName!, $"a defined \"{typeof(T)}\" enumeration value");

        return argument;
    }

    public static T ThrowIfNull<T>(this T argument,
        [CallerArgumentExpression(nameof(argument))] string? argName = null)
        where T: class
    {
        if (argument is null)
            MayNot(argName!, "null");

        return argument!;
    }

    private static void Must(string argName, string suffix) =>
        throw new LogSmartException($"The \"{argName}\" field must be {suffix}.");

    private static void MayNot(string argName, string suffix) =>
        throw new LogSmartException($"The \"{argName} field \" may not be {suffix}.");
}