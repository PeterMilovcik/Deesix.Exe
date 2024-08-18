using Deesix.Application.Interfaces;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;

namespace Deesix.Application.Actions;

public class GenerateWorldDescriptionAction(IGenerator generator) : IAction
{
    private readonly IGenerator generator = generator
        ?? throw new ArgumentNullException(nameof(generator));

    public string Title => "Generate world description";

    public string ProgressTitle => "Generating world description...";

    public int Order => 1;

    public bool CanExecute(Turn turn) =>
        turn.Game.HasValue && 
        turn.Game.Value.World is not null &&
        string.IsNullOrWhiteSpace(turn.Game.Value.World.Description) &&
        !string.IsNullOrWhiteSpace(turn.Game.Value.World.Genre) && 
        turn.Game.Value.World.WorldSettings is not null &&
        turn.LastAction is AcceptGeneratedWorldSettingsAction;

    public async Task<Turn> ExecuteAsync(Turn turn)
    {
        if (CanExecute(turn))
        {            
            turn.Actions.Clear();
            var result = await generator.World.GenerateWorldDescriptionsAsync(turn.Game.Value.World!, count: 5);
            if (result.IsSuccess)
            {
                turn.Message = "Please choose a description for the world.";
                turn.Question = "Which description do you want to use?";
                foreach (var description in result.Value)
                {
                    turn.Actions.Add(new AcceptGeneratedWorldDescriptionAction(description));
                }
            }
            else
            {
                turn.Message = "Failed to generate world description.";
                turn.Question = "How do you want to proceed?";
                //turn.Actions.Add(new RetryGenerateWorldDescriptionAction());
            }
        }
        return turn;
    }
}
