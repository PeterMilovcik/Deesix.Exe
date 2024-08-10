using CSharpFunctionalExtensions;
using Deesix.Application.Actions;
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
        IAction? gameOption = null;
        while (gameOption is not ExitAction)
        {
            DisplayGameState(turn);
            gameOption = SelectGameOption();
            
            await AnsiConsole.Status().StartAsync(gameOption.ProgressTitle, async ctx =>
            {
                await gameMaster.ProcessActionAsync(gameOption);
            });

            gameMaster.Turn.Game.Execute(game => gameRepository.SaveChanges());
            turn++;
        }
    }

    private IAction SelectGameOption()
    {
        var options = gameMaster.Turn.Actions;

        var option = AnsiConsole.Prompt(
            new SelectionPrompt<IAction>()
                .UseConverter(option => option.Title)
                .Title(gameMaster.Turn.Question)
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more choices)[/]")
                .AddChoices(options));
        return option;
    }

    private void DisplayGameState(int turn)
    {
        AnsiConsole.Write(new Rule($"Turn {turn}") { Justification = Justify.Left });
        AnsiConsole.MarkupLine($"[bold]Game Master says[/]: {gameMaster.Turn.Message}");
    }
}