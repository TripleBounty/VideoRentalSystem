using System.Collections.Generic;

namespace VideoRentalSystem.Commands.Contracts
{
    public interface ICommand
    {
        string Execute(IList<string> parameters);
    }
}
