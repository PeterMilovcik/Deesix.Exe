using Deesix.AI.OpenAI;
using Deesix.Exe.Core;

namespace Deesix.Exe.Factories;

public class RealmFactory(UserInterface ui, Generators generators)
{
    private readonly Generators generators = generators ?? throw new ArgumentNullException(nameof(generators));
    private readonly UserInterface ui = ui ?? throw new ArgumentNullException(nameof(ui));

    public async Task<Realm> CreateRealmAsync(World world)
    {
        var realmId = Guid.NewGuid().ToString();
        string realmDescription = "A realm of mystery and wonder.";
        string realmName = "Realm of the Unknown";

        await ui.ShowProgressAsync("Generating realm...", async ctx =>
        {
            var realmDescriptionResult = await generators.Realm.GenerateRealmDescriptionAsync(world);
            
            if (realmDescriptionResult.Success)
            {
                realmDescription = realmDescriptionResult.Value!;
                var realmNameResult = await generators.Realm.GenerateRealmNameAsync(realmDescription);
                
                if (realmNameResult.Success)
                {
                    realmName = realmNameResult.Value!;
                }
                else
                {
                    ui.ErrorMessage(realmNameResult.ErrorMessage);
                }
            }
            else
            {
                ui.ErrorMessage(realmDescriptionResult.ErrorMessage);
            }
        });

        return new Realm
        {
            Id = realmId,
            Path = $"{world.Id}/{realmId}",
            Name = realmName,
            Description = realmDescription,
            World = world
        };
    }
}
