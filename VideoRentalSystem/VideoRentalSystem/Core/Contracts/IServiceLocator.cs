using VideoRentalSystem.Commands.Contracts;

namespace VideoRentalSystem.Core.Contracts
{
    public interface IServiceLocator
    {
        ICommand GetCommand(string commandName);
    }
}
