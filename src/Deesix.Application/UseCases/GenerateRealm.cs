using CSharpFunctionalExtensions;
using Deesix.Application.Interfaces;
using Deesix.Domain.Entities;
using Deesix.Domain.Utilities;

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
        public required Result<GeneratedRealm> Realm { get; init; }

        public class GeneratedRealm
        {
            [JsonPropertyMetadata("string", "Short memorable name of the realm. Max. 30 characters.")]
            public required string Name { get; init; }
            [JsonPropertyMetadata("string", "Concise description of the realm. Max. 300 characters.")]
            public required string Description { get; init; }
        }
    }

    public async Task<Response> ExecuteAsync(Request request) => 
        await generator.Realm.GenerateRealmAsync(request);
}
