using Deesix.Domain.Interfaces;

namespace Deesix.Domain.Entities;

public sealed class GameMaster : IGameMaster
{
    public Turn Turn { get; private set; }
    private readonly IGameOptionFactory gameOptionFactory;

    public GameMaster(IGameOptionFactory gameOptionFactory)
    {
        Turn = new Turn();
        this.gameOptionFactory = gameOptionFactory;
        var options = gameOptionFactory
            .CreateGameOptions(Turn)
            .Where(option => option.CanExecute(Turn));
        Turn.GameActions.AddRange(options);
    }

    public async Task ProcessGameActionAsync(IGameAction option)
    {
        Turn = await option.ExecuteAsync(Turn);
        Turn.LastGameAction = option;
        var generalGameOptions = gameOptionFactory
            .CreateGameOptions(Turn)
            .Where(option => option.CanExecute(Turn));
        Turn.GameActions.AddRange(generalGameOptions);
    }
}
