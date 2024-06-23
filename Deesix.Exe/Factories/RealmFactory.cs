using CSharpFunctionalExtensions;
using Deesix.AI;
using Deesix.Core;

namespace Deesix.Exe.Factories;

public class RealmFactory(UserInterface ui, Generators generators)
{
    private readonly Generators generators = generators ?? throw new ArgumentNullException(nameof(generators));
    private readonly UserInterface ui = ui ?? throw new ArgumentNullException(nameof(ui));

    public async Task<Result<Realm>> CreateRealmAsync(World world)
    {
        var realmId = Guid.NewGuid().ToString();
        string realmDescription = "A realm of mystery and wonder.";
        string realmName = "Realm of the Unknown";
        Realm? realm = null;
        await ui.ShowProgressAsync("Generating realm...", async ctx =>
        {
            var realmDescriptionResult = await generators.Realm.GenerateRealmDescriptionAsync(world);
            
            if (realmDescriptionResult.IsSuccess)
            {
                realmDescription = realmDescriptionResult.Value!;
                var realmNameResult = await generators.Realm.GenerateRealmNameAsync(realmDescription);
                
                if (realmNameResult.IsSuccess)
                {
                    realmName = realmNameResult.Value!;

                    realm = new Realm
                    {
                        Id = realmId,
                        Path = $"{world.Id}/{realmId}",
                        Name = realmName,
                        Description = realmDescription,
                        World = world
                    };
                }
                else
                {
                    ui.ErrorMessage(realmNameResult.Error);
                }
            }
            else
            {
                ui.ErrorMessage(realmDescriptionResult.Error);
            }
        });
        return realm is null 
            ? Result.Failure<Realm>("Realm not created.") 
            : Result.Success(realm);
    }
}
