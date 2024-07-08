using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Spectre.Console;

internal class Program
{
    private static async Task Main(string[] args)
    {
        try
        {
            var host = Host
                .CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) => services.AddDeesix()).Build();
    
            var gameLoop = host.Services.GetRequiredService<GameLoop>();
            await gameLoop.StartAsync();
        }
        catch (Exception exception)
        {
            AnsiConsole.MarkupLine($"[red]An error occurred: {exception.Message}[/]");
        }
    }
}
