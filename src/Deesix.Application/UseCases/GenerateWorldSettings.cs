using CSharpFunctionalExtensions;
using Deesix.Application.Interfaces;
using Deesix.Domain.Entities;

namespace Deesix.Application;

public class GenerateWorldSettings
{
    private readonly IGenerator _generator;

    public class Request
    {
        public required List<string> WorldThemes { get; init; }
    }
    
    public class Response
    {
        public required Result<WorldSettings> WorldSettings { get; init; }
    }

    public GenerateWorldSettings(IGenerator generator)
    {
        _generator = generator ?? throw new ArgumentNullException(nameof(generator));
    }

    public async Task<Response> ExecuteAsync(Request request) => new Response
    {
        WorldSettings = await _generator.World.GenerateWorldSettingsAsync(request.WorldThemes)
    };
}
