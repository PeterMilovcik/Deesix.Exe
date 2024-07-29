using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;

namespace Deesix.Application.Actions;

public class GenerateWorldSettings : IAction
{
    public string Title => "Generate World Settings";

    public int Order => 1;

    public bool CanExecute(Turn turn)
    {
        throw new NotImplementedException();
    }

    public Task<Turn> ExecuteAsync(Turn turn)
    {
        throw new NotImplementedException();
    }
}
