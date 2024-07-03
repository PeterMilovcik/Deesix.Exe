using CSharpFunctionalExtensions;
using Deesix.Application.Interfaces;
using Deesix.Domain.Entities;

namespace Deesix.Application;

public sealed class GenerateLocation(IGenerator generator)
{
    private readonly IGenerator generator = generator;

    public sealed class Request
    {
        public required Region Region { get; init; }
    }

    public sealed class Response
    {
        public required Result<Location> Location { get; init; }
    }

    public async Task<Response> ExecuteAsync(Request request) => new Response
    {
        Location = await generator.Location.GenerateLocationAsync(request.Region)
    };
}
