using CSharpFunctionalExtensions;
using Deesix.Domain.Entities;

namespace Deesix.Application;

public sealed class CreateRealm
{
    public sealed class Request
    {
        public required int WorldId { get; init; }
        public required GenerateRealm.Response.GeneratedRealm GeneratedRealm { get; init; }
    }

    public sealed class Response
    {
        public required Result<Realm> Realm { get; init; }
    }

    public Response Execute(Request request)
    {
        var realm = new Realm
        {
            WorldId = request.WorldId,
            Name = request.GeneratedRealm.Name,
            Description = request.GeneratedRealm.Description
        };

        return new Response { Realm = Result.Success(realm) };
    }
}
