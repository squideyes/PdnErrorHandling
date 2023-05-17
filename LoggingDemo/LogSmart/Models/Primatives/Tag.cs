// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.PdnLogSmart.LogSmart;

public readonly struct Tag : IEquatable<Tag>, IComparable<Tag>
{
    private Tag(string value)
    {
        Value = value;
    }

    private string Value { get; }

    public static Tag From(string value)
    {
        value.ThrowIfInvalid(v => v.IsTagValue());

        return new Tag(value);
    }

    public int CompareTo(Tag other) => Value.CompareTo(other.Value);

    public bool Equals(Tag other)
    {
        if (Value is null)
            return other.Value is null;

        return Value.Equals(other.Value);
    }

    public override string ToString() => Value;

    public override bool Equals(object? other) =>
        other is Tag tag && Equals(tag);

    public override int GetHashCode() => Value.GetHashCode();

    public static bool operator ==(Tag left, Tag right)=>
        left.Equals(right);

    public static bool operator !=(Tag left, Tag right) =>
        !(left == right);

    public static bool operator <(Tag left, Tag right) =>
        left.CompareTo(right) < 0;

    public static bool operator <=(Tag left, Tag right) =>
        left.CompareTo(right) <= 0;

    public static bool operator >(Tag left, Tag right) =>
        left.CompareTo(right) > 0;

    public static bool operator >=(Tag left, Tag right) =>
        left.CompareTo(right) >= 0;
}