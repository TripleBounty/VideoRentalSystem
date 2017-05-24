using System;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Models.Factories;

namespace VideoRentalSystem.Commands.Factory
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
                case "createcountry":
                    return this.CreateCountryCommand();
                default:
                    throw new Exception("The passed command is not valid!");
            }
        }

        public ICommand CreateUserCommand()
        {
            return new CreateUserCommand(this.database, this.factory);
        }

        public ICommand CreateCountryCommand()
        {
            return new CreateCountryCommand(this.database, this.factory);
        }


    }
}
