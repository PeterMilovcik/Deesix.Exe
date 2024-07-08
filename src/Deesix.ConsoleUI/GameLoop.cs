using CSharpFunctionalExtensions;
using Deesix.Application.Interfaces;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;

internal class GameLoop(IGameMaster gameMaster, IRepository<Game> gameRepository)
{
    private readonly IGameMaster gameMaster = gameMaster ?? throw new ArgumentNullException(nameof(gameMaster));
    private readonly IRepository<Game> gameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));

    public async Task StartAsync()
    {
        int turn = 1;
        while(true)
        {
            Console.WriteLine($"Turn: {turn}");
            Console.WriteLine($"Game Master says: {gameMaster.GetMessage()}");
            Console.WriteLine($"Game Master asks: {gameMaster.GetQuestion()}");

            // Call GetOptions() and show them to the console
            IGameOption[] options = gameMaster.GetOptions();
            for (int i = 0; i < options.Length; i++)
            {
                if (options[i].CanExecute(gameMaster.Game))
                {
                    Console.WriteLine($"{i + 1}. {options[i].Description}");
                }
            }

            // Read the input from the user - player for these options
            int selectedOption;
            if (int.TryParse(Console.ReadLine(), out selectedOption) && selectedOption > 0 && selectedOption <= options.Length)
            {
                var option = options[selectedOption - 1];
                if (option.CanExecute(gameMaster.Game))
                {
                    await gameMaster.ProcessOptionAsync(option);
                }
                else
                {
                    Console.WriteLine("This option cannot be executed right now.");
                }
            }
            else
            {
                Console.WriteLine("Invalid option, please try again.");
            }

            gameMaster.Game.Execute(game => gameRepository.Update(game));
            
            turn++;
        }
    }
}