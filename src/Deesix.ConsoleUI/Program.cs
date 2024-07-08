using Deesix.Application;
using Deesix.Application.Interfaces;
using Deesix.ConsoleUI;
using Deesix.Domain.Entities;
using Deesix.Infrastructure;
using Deesix.Infrastructure.DataAccess;
using Deesix.Infrastructure.Generators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var host = Host
            .CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) => services.AddDeesix()).Build();

        var gameLoop = host.Services.GetRequiredService<GameLoop>();
        await gameLoop.StartAsync();
        return;

        using var serviceScope = host.Services.CreateScope();
        var services = serviceScope.ServiceProvider;

        try
        {
            var ui = services.GetRequiredService<IUserInterface>();
            var openAiApiKey = services.GetRequiredService<IOpenAIApiKey>();
            var worldRepository = services.GetRequiredService<IRepository<World>>();
            var realmRepository = services.GetRequiredService<IRepository<Realm>>();
            var generator = services.GetRequiredService<IGenerator>();
            var newGame = services.GetRequiredService<NewGame>();

            ui.DisplayGameTitleAndDescription();

            openAiApiKey.CheckOpenAiApiKey();

            const string newGameOption = "New Game";
            const string loadGameOption = "Load Game";
            const string exitOption = "Exit";
            var menu = ui.SelectFromOptions("Main Menu", new List<string> { newGameOption, loadGameOption, exitOption });

            switch (menu)
            {
                case newGameOption:
                    await newGame.ExecuteAsync();
                    break;
                case loadGameOption:
                    // Load game
                    break;
                case exitOption:
                    ui.Clear();
                    return;
            }
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred.");
        }
    }
}
