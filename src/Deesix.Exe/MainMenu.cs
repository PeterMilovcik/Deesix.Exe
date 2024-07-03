namespace Deesix.Exe;

public class MainMenu(GameManager gameManager, UserInterface ui)
{    
    private readonly GameManager gameManager = gameManager ?? throw new ArgumentNullException(nameof(gameManager));
    private readonly UserInterface ui = ui ?? throw new ArgumentNullException(nameof(ui));

    public const string Title = "Main Menu";
    public const string NewGame = "New Game";
    public const string LoadGame = "Load Game";
    public const string Exit = "Exit";

    public string Show()
    {
        var options = new List<string> { NewGame };
        if (gameManager.LoadGameFiles().Any()) options.Add(LoadGame);
        options.Add(Exit);
        return ui.SelectFromOptions(Title, options);
    }
}
