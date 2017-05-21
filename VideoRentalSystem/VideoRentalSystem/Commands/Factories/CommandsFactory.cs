using System;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data;
using VideoRentalSystem.Models.Factories;

namespace VideoRentalSystem.Commands.Factories
{
    public class CommandsFactory : ICommandsFactory
    {
        private readonly IDatabase database;
        private readonly IModelsFactory factory;

        public CommandsFactory(IDatabase database, IModelsFactory factory)
        {
            this.database = database;
            this.factory = factory;
        }

        public ICommand CreateCommandFromString(string commandName)
        {
            switch (commandName.ToLower())
            {
                case "createuser":
                    return this.CreateUserCommand();
                default:
                    throw new Exception("The passed command is not valid!");
            }
        }

        public ICommand CreateUserCommand()
        {
            return new CreateUserCommand(this.database, this.factory);
        }


    }
}
