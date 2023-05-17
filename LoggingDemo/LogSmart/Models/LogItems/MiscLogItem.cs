// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.PdnLogSmart.LogSmart;

public class MiscLogItem : LogItemBase
{
    private readonly Context? context;

    public MiscLogItem(Severity severity, Tag activity, Context context = null!)
        : base(severity, activity)
    {
        context = context.ThrowIfInvalid(v => v is null || !v.IsEmpty);
    }

    public override (Tag, object)[] GetTagValues()
    {
        if (context == null)
            return Array.Empty<(Tag, object)>();
        else
            return context!.Select(kv => (kv.Key, kv.Value)).ToArray();
    }
}