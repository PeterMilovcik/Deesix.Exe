using CSharpFunctionalExtensions;
using Deesix.Application.Interfaces;
using Deesix.Domain.Entities;

namespace Deesix.Application;

public sealed class GenerateWorldSettings(IGenerator generator)
{
    private readonly IGenerator generator = generator ?? throw new ArgumentNullException(nameof(generator));

    public sealed class Request
    {
        public required List<string> WorldThemes { get; init; }
    }
    
    public sealed class Response
    {
        public required Result<WorldSettings> WorldSettings { get; init; }
    }
    
    public async Task<Response> ExecuteAsync(Request request) => new Response
    {
        WorldSettings = await generator.World.GenerateWorldSettingsAsync(request.WorldThemes)
    };

    public object Execute(Request request)
    {
        throw new NotImplementedException();
    }
}
