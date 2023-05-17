// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using Serilog.Events;
using System.Diagnostics;
using System.Text;

namespace SquidEyes.PdnLogSmart.LogSmart;

internal static class MiscExtenders
{
    public static R Convert<T, R>(this T value, Func<T, R> getValue) =>
        getValue(value);

    public static bool IsDefault<T>(this T value) =>
        Equals(value, default(T));

    public static LogEventLevel ToLogEventLevel(this Severity logLevel)
    {
        return logLevel switch
        {
            Severity.Debug => LogEventLevel.Debug,
            Severity.Info => LogEventLevel.Information,
            Severity.Warn => LogEventLevel.Warning,
            Severity.Error => LogEventLevel.Error,
            _ => throw new LogSmartException(
                "A defined \"severity\" must be supplied.")
        };
    }

    public static bool IsTagValue(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        if (!char.IsAsciiLetterUpper(value[0]))
            return false;

        if (value.Length == 1)
            return true;

        if (value.Length > 24)
            return false;

        return value.Skip(1).All(char.IsAsciiLetterOrDigit);
    }

    public static string[] GetStackTrace(this Exception error, bool withFileInfo)
    {
        var result = new List<string>();

        var stackTrace = new StackTrace(error, withFileInfo);

        foreach (var f in stackTrace.GetFrames())
        {
            var sb = new StringBuilder();

            var method = f.GetMethod();

            if (method is null)
                continue;

            sb.Append(method);

            var fileName = f.GetFileName();

            if (withFileInfo && !string.IsNullOrWhiteSpace(fileName))
            {
                var line = f.GetFileLineNumber();
                var column = f.GetFileColumnNumber();

                sb.Append($", {fileName} (Line {line}, Column {column})");
            }

            result.Add(sb.ToString());
        }

        return result.ToArray();
    }
}