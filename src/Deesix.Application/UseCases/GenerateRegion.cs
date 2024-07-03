using CSharpFunctionalExtensions;
using Deesix.Application.Interfaces;
using Deesix.Domain.Entities;

namespace Deesix.Application;

public sealed class GenerateRegion(IGenerator generator)
{
    private readonly IGenerator generator = generator;

    public sealed class Request
    {
        public required Realm Realm { get; init; }
    }

    public sealed class Response
    {
        public required Result<Region> Region { get; init; }
    }

    public async Task<Response> ExecuteAsync(Request request) => new Response
    {
        Region = await generator.Region.GenerateRegionAsync(request.Realm)
    };
}
