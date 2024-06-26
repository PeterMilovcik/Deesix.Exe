namespace Deesix.Core;

public class Skill
{
    public static Skill Create(string name, int level) => new Skill
    {
        Name = SkillName.Create(name),
        Level = SkillLevel.Create(level)    
    };

    private Skill()
    {
    }

    public required SkillName Name { get; init; }
    public required SkillLevel Level { get; init; }

    public List<int> Roll() 
    {
        var rolls = new List<int>();
        for (var i = 0; i < Level.Value; i++)
        {
            rolls.Add(Dice.Roll());
        }
        return rolls;
    }
}
