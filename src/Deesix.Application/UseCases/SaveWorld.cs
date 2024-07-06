using CSharpFunctionalExtensions;
using Deesix.Application.Interfaces;
using Deesix.Domain.Entities;

namespace Deesix.Application.UseCases;

public sealed class SaveWorld(IRepository<World> repository, IUserInterface userInterface)
{
    private readonly IRepository<World> repository = repository;
    private readonly IUserInterface userInterface = userInterface;
    
    public Task<Result<World>> ExecuteAsync(World world)
    {
        try
        {
            var savedWorld = repository.Add(world);
            return Task.FromResult(savedWorld is not null
                ? Result.Success(savedWorld)
                : Result.Failure<World>("Error saving world"));
        }
        catch (Exception exception)
        {
            return Task.FromResult(Result.Failure<World>($"Error saving world: {exception.Message}"));
        }
    }
}
