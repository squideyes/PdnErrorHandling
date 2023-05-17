// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.PdnLogSmart.LogSmart;

public abstract class LogItemBase
{
    private static int ordinal = -1;

    public LogItemBase(Severity severity, Tag activity = default)
    {
        Ordinal = Interlocked.Increment(ref ordinal);

        Severity = severity.ThrowIfNotDefined();

        Activity = activity == default ? 
            Tag.From(GetType().Name) : activity.ThrowIfDefault();
    }

    public int Ordinal { get; }
    public Severity Severity { get; }
    public Tag Activity { get; }

    public abstract (Tag, object)[] GetTagValues();

    public virtual void Validate()
    {
    }
}