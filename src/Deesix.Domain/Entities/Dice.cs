
namespace Deesix.Domain.Entities;

public class Dice
{
    public static int Roll() => new Random().Next(1, 7);
}