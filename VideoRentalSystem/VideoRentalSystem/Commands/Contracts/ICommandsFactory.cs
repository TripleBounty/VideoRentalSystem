namespace VideoRentalSystem.Commands.Contracts
{
    public interface ICommandsFactory
    {
        ICommand CreateCommandFromString(string commandName);
    }
}
