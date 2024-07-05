using CSharpFunctionalExtensions;
using Deesix.Application.Interfaces;
using Deesix.Domain.Entities;

namespace Deesix.Application.UseCases;

public sealed class SaveWorld(IRepository<World> repository)
{
    private readonly IRepository<World> repository = repository;

    public sealed class Request
    {
        public required World World { get; init; }
    }

    public sealed class Response
    {
        public required Result<World> World { get; init; }
    }

    public Task<Response> ExecuteAsync(Request request)
    {
        try
        {
            var savedWorld = repository.Add(request.World);
            return Task.FromResult(savedWorld is not null
                ? new Response { World = Result.Success(savedWorld) }
                : new Response { World = Result.Failure<World>("Error saving world") });
        }
        catch (Exception exception)
        {
            return Task.FromResult(new Response { World = Result.Failure<World>($"Error saving world: {exception.Message}") });
        }
    }
}
