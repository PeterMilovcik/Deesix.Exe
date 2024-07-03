using CSharpFunctionalExtensions;
using Deesix.Application.Interfaces;
using Deesix.Domain.Entities;

namespace Deesix.Application;

public sealed class GenerateWorld(IGenerator generator)
{
    private readonly IGenerator generator = generator ?? throw new ArgumentNullException(nameof(generator));

    public sealed class Request
    {
        public required WorldSettings WorldSettings { get; init; }
    }

    public sealed class Response
    {
        public required Result<World> World { get; init; }
    }

    public async Task<Response> ExecuteAsync(Request request) => new Response
    {
        World = await generator.World.GenerateWorldAsync(request.WorldSettings)
    };
}
