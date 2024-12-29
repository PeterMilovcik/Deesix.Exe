﻿using Deesix.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Spectre.Console;
using Deesix.Product.Infrastructure;

namespace Deesix.ConsoleUI;

internal class Program
{
    private static async Task Main(string[] args)
    {
        try
        {
            var host = Host
                .CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    string connectionString = "Data Source=deesix.db";
                    services.AddDeesixInfrastructure(connectionString);
                }).Build();
            
            using (var scope = host.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();       // Applies any pending migrations
            }
    
            var gameLoop = host.Services.GetRequiredService<GameLoop>();
            await gameLoop.StartAsync();
        }
        catch (Exception exception)
        {
            AnsiConsole.MarkupLine($"[red]An error occurred: {exception.Message}[/]");
        }
    }
}
