namespace Deesix.Core;

public class SkillLevel
{
    public static SkillLevel Create(int value)
    {
        if (value < 1)
            throw new ArgumentException("Skill value must be greater than 0.", nameof(value));
        return new SkillLevel(value);
    }
    
    private SkillLevel(int value) => Value = value;
    public int Value { get; init; }
}