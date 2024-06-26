
namespace Deesix.Core;

public class Dice
{
    public static int Roll() => new Random().Next(1, 7);
}