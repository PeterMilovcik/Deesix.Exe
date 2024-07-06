using CSharpFunctionalExtensions;
using Deesix.Application.Interfaces;
using Deesix.Domain.Entities;

namespace Deesix.Application.UseCases;

public sealed class SaveRealm(IRepository<Realm> repository, IUserInterface userInterface)
{
    private readonly IRepository<Realm> repository = repository;
    private readonly IUserInterface userInterface = userInterface;
    
    public async Task<Result<Realm>> ExecuteAsync(Realm realm)
    {
        try
        {
            return await userInterface.ShowProgressAsync("Saving realm...", () => 
            {
                var savedRealm = repository.Add(realm);
                return Task.FromResult(savedRealm is not null
                    ? Result.Success(savedRealm)
                    : Result.Failure<Realm>("Error saving realm"));
            });
        }
        catch (Exception exception)
        {
            return Result.Failure<Realm>($"Error saving realm: {exception.Message}");
        }
    }
}
