using CSharpFunctionalExtensions;
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

    public async Task<Response> ExecuteAsync(Request request)
    {
        try
        {
            var savedWorld = await repository.InsertAsync(request.World);
            return savedWorld is not null
                ? new Response { World = Result.Success(savedWorld) }
                : new Response { World = Result.Failure<World>("Error saving world") };
        }
        catch (Exception exception)
        {
            return new Response { World = Result.Failure<World>($"Error saving world: {exception.Message}") };
        }
    }
}
