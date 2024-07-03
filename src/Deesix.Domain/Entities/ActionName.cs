namespace Deesix.Domain.Entities;

public class ActionName
{
    public static ActionName Create(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));
        return new ActionName(name);
    }

    public string Value { get; init; }

    private ActionName(string name) => Value = name;

    public override string ToString() => Value;
}