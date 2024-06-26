namespace Deesix.Core;

public class SkillName
{
    public static SkillName Create(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));
        return new SkillName(name);
    }

    public string Value { get; init; }

    private SkillName(string name) => Value = name;

    public override string ToString() => Value;
}