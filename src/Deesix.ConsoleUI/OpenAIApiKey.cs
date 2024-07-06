using CSharpFunctionalExtensions;
using Deesix.Infrastructure;
using Spectre.Console;
using System.Text.Json;

namespace Deesix.ConsoleUI;

internal class OpenAIApiKey : IOpenAIApiKey
{
    private const string OpenAiFileName = "deesix.openai";
    private readonly string baseDirectory;

    public OpenAIApiKey()
    {
        baseDirectory = AppContext.BaseDirectory;
    }

    public string? GetOpenAiApiKey()
    {
        var openAiFilePath = Path.Combine(baseDirectory, OpenAiFileName);
        return !File.Exists(openAiFilePath) 
            ? ReadFromConsoleAndSaveToFile(openAiFilePath) 
            : ReadFromFile(openAiFilePath);
    }

    private static string? ReadFromFile(string openAiFilePath)
    {
        var jsonText = File.ReadAllText(openAiFilePath);
        var doc = JsonDocument.Parse(jsonText);
        var root = doc.RootElement;

        try
        { 
            return root.GetProperty("apiKey").ToString();
        }
        catch (KeyNotFoundException ex)
        {
            AnsiConsole.MarkupLine($"[red]OpenAI API key not found in the '{OpenAiFileName}' file: {0}[/]", ex.Message);
            return string.Empty;
        }
        catch (InvalidOperationException ex)
        {
            AnsiConsole.MarkupLine($"[red]Error reading OpenAI API key from '{OpenAiFileName}' file: {0}[/]", ex.Message);
            return string.Empty;
        }
    }

    private static string ReadFromConsoleAndSaveToFile(string openAiFilePath)
    {
        string? openAiApiKey = ReadFromConsole();
        SaveApiKeyToFile(openAiFilePath, openAiApiKey);
        return openAiApiKey;
    }

    private static string ReadFromConsole()
    {
        string? openAiApiKey;
        AnsiConsole.MarkupLine($"[gray]'{OpenAiFileName}' file not found in the base directory.[/]");
        AnsiConsole.MarkupLine("[yellow]Deesix.exe[/] requires an [yellow]OpenAI API key[/] to generate content using [yellow]gpt-3.5-turbo[/] model.");
        AnsiConsole.MarkupLine("You can obtain an API key by signing up at [blue]https://platform.openai.com/api-keys[/].");
        openAiApiKey = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter your [green]OpenAI API key[/]:")
                .PromptStyle("green")
                .Secret()
                .ValidationErrorMessage("[red]Not a valid OpenAI API key[/]")
                .Validate(input =>
                {
                    if (string.IsNullOrWhiteSpace(input))
                    {
                        return ValidationResult.Error("[red]OpenAI API key cannot be empty[/]");
                    }
                    if (!input.StartsWith("sk-"))
                    {
                        return ValidationResult.Error("[red]OpenAI API key must start with 'sk-'[/]");
                    }
                    return ValidationResult.Success();
                }));
        return openAiApiKey;
    }

    private static void SaveApiKeyToFile(string openAiFilePath, string? openAiApiKey)
    {
        var openAiJson = new { apiKey = openAiApiKey };
        var openAiJsonText = JsonSerializer.Serialize(openAiJson, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(openAiFilePath, openAiJsonText);
        AnsiConsole.MarkupLine($"[gray]'{OpenAiFileName}' file created in the base directory.[/]");
    }

    public Result CheckOpenAiApiKey()
    {
        var openAiFilePath = Path.Combine(baseDirectory, OpenAiFileName);
        var openAiApiKey = !File.Exists(openAiFilePath) 
            ? ReadFromConsoleAndSaveToFile(openAiFilePath) 
            : ReadFromFile(openAiFilePath);
        return string.IsNullOrWhiteSpace(openAiApiKey) || !openAiApiKey.StartsWith("sk-")
            ? Result.Failure("OpenAI API key is invalid")
            : Result.Success();
    }
}
