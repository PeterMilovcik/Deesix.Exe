using CSharpFunctionalExtensions;
using Deesix.Application.Interfaces;
using Deesix.Domain.Entities;

namespace Deesix.Application.UseCases;

public sealed class SaveRealm(IRepository<Realm> repository)
{
    private readonly IRepository<Realm> repository = repository;

    public sealed class Request
    {
        public required Realm Realm { get; init; }
    }

    public sealed class Response
    {
        public required Result<Realm> Realm { get; init; }
    }

    public Task<Response> ExecuteAsync(Request request)
    {
        try
        {
            var savedRealm = repository.Add(request.Realm);
            return Task.FromResult(savedRealm is not null
                ? new Response { Realm = Result.Success(savedRealm) }
                : new Response { Realm = Result.Failure<Realm>("Error saving realm") });
        }
        catch (Exception exception)
        {
            return Task.FromResult(new Response { Realm = Result.Failure<Realm>($"Error saving realm: {exception.Message}") });
        }
    }
}
