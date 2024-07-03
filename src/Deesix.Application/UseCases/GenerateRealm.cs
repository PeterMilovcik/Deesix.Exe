using Deesix.Application.Interfaces;
using Deesix.Domain.Entities;

namespace Deesix.Application;

public sealed class GenerateRealm(IGenerator generator)
{
    private readonly IGenerator generator = generator;

    public sealed class Request
    {
        public required World World { get; init; }
    }

    public sealed class Response
    {
        public required Realm Realm { get; init; }
    }

    public async Task<Response> ExecuteAsync(Request request) => new Response
    {
        Realm = await generator.Realm.GenerateRealmAsync(request.World)
    };
}
