using Deesix.Application.Actions;
using Deesix.Application.Interfaces;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;

namespace Deesix.Application;

public class RegenerateWorldSettingsAction(IGenerator generator) : IAction
{
    private readonly IGenerator generator = generator 
        ?? throw new ArgumentNullException(nameof(generator));

    public string Title => "Regenerate World Settings";

    public int Order => 2;

    public bool CanExecute(Turn turn) => 
        turn.Game.HasValue &&
        turn.Game.Value.World is not null &&
        !string.IsNullOrWhiteSpace(turn.Game.Value.World.Genre) &&
        turn.Game.Value.World.WorldSettings is not null &&
        turn.LastAction is GenerateWorldSettingsAction or RegenerateWorldSettingsAction;

    public async Task<Turn> ExecuteAsync(Turn turn)
    {
        if (CanExecute(turn))
        {
            var result = await generator.World.GenerateWorldSettingsAsync(turn.Game.Value.World!.Genre!);
            if (result.IsSuccess)
            {
                turn.Game.Value.World.WorldSettings = result.Value;
                turn.Message = $"World Settings: {result.Value}";
                turn.Question = "How do you want to proceed?";
                turn.Actions.Clear();
            }
        }
        return turn;
    }
}
