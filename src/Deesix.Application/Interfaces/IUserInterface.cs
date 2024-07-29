namespace Deesix.Application;

public interface IUserInterface
{
    void DisplayGameTitleAndDescription();
    string SelectFromOptions(string title, List<string> options);
    string PromptGenre();
    void Clear();
    void ErrorMessage(string message);
    void SuccessMessage(string message);
    Task<T> ShowProgressAsync<T>(string message, Func<Task<T>> func);
}
