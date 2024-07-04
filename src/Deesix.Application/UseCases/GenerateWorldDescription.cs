using CSharpFunctionalExtensions;
using Deesix.Application.Interfaces;
using Deesix.Domain.Entities;

namespace Deesix.Application;

public sealed class GenerateWorldDescription(IGenerator generator)
{
    private readonly IGenerator generator = generator ?? throw new ArgumentNullException(nameof(generator));

    public sealed class Request
    {
        public required WorldSettings WorldSettings { get; init; }
    }

    public sealed class Response
    {
        public required Result<string> WorldDescription { get; init; }
    }

    public async Task<Response> ExecuteAsync(Request request) => new Response
    {
        WorldDescription = await generator.World.GenerateWorldDescriptionAsync(request.WorldSettings)
    };
}
