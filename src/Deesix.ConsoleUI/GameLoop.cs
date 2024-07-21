using CSharpFunctionalExtensions;
using Deesix.Application.GameOptions;
using Deesix.Application.Interfaces;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;
using Spectre.Console;

internal class GameLoop(IGameMaster gameMaster, IRepository<Game> gameRepository)
{
    private readonly IGameMaster gameMaster = gameMaster ?? 
        throw new ArgumentNullException(nameof(gameMaster));
    private readonly IRepository<Game> gameRepository = gameRepository ?? 
        throw new ArgumentNullException(nameof(gameRepository));

    public async Task StartAsync()
    {
        int turn = 1;
        IGameOption? gameOption = null;
        while (gameOption is not ExitGameOption)
        {
            DisplayGameState(turn);
            gameOption = SelectGameOption();
            await gameMaster.ProcessOptionAsync(gameOption);
            gameMaster.GameTurn.Game.Execute(game => gameRepository.Update(game));
            turn++;
        }
    }

    private IGameOption SelectGameOption()
    {
        var options = gameMaster.GameTurn.GameOptions;

        var option = AnsiConsole.Prompt(
            new SelectionPrompt<IGameOption>()
                .UseConverter(option => option.Title)
                .Title(gameMaster.GameTurn.Question)
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more choices)[/]")
                .AddChoices(options));
        return option;
    }

    private void DisplayGameState(int turn)
    {
        AnsiConsole.Write(new Rule($"Turn {turn}") { Justification = Justify.Left });
        AnsiConsole.MarkupLine($"[bold]Game Master says[/]: {gameMaster.GameTurn.Message}");
    }
}