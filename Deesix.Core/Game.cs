using CSharpFunctionalExtensions;

namespace Deesix.Core;

public class Game
{
    public required string Id { get; set; }
    public required World World { get; set; }
    public required Character Character { get; set; }
    public DateTime CurrentTime { get; set; }

    public async Task<Result> ProcessActionAsync(IAction action)
    {
        var result = await action.ExecuteAsync(this);

        if (result.IsSuccess)
        {
            CurrentTime = CurrentTime.Add(action.Duration);
            return Result.Success();
        }

        return result;
    }
}