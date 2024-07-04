using Deesix.Domain.Entities;

namespace Deesix.Application;

public sealed class CreateWorld
{
    public sealed class Request
    {
        public required string WorldName { get; init; }
        public required string WorldDescription { get; init; }
        public required WorldSettings WorldSettings { get; init; }
    }

    public sealed class Response
    {
        public required World World { get; init; }
    }

    public Task<Response> ExecuteAsync(Request request)
    {
        var world = new World
        {
            Name = request.WorldName,
            Description = request.WorldDescription,
            WorldSettings = request.WorldSettings,
        };

        return Task.FromResult(new Response { World = world });
    }
}
