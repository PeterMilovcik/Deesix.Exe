using CSharpFunctionalExtensions;
using Deesix.Application.Interfaces;

namespace Deesix.Application;

public sealed class GenerateWorldNames(IGenerator generator)
{
    private readonly IGenerator generator = generator ?? throw new ArgumentNullException(nameof(generator));

    public sealed class Request
    {
        public required string WorldDescription { get; init; }
        public required int Count { get; init; }
    }

    public sealed class Response
    {
        public required Result<List<string>> WorldNames { get; init; }
    }

    public async Task<Response> ExecuteAsync(Request request) => new Response
    {
        WorldNames = await generator.World.GenerateWorldNamesAsync(request.WorldDescription, request.Count)
    };
}