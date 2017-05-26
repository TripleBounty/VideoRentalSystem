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
                case "createcountry":
                    return this.CreateCountryCommand();
                case "createemployee":
                    return this.CreateEmployeesCommand();
                default:
                    throw new Exception("The passed command is not valid!");
            }
        }

        private ICommand CreateCountryCommand()
        {
            return new CreateCountryCommand(this.database, this.factory);
        }

        private ICommand CreateEmployeesCommand()
        {
            return new CreateEmployeesCommand(this.database, this.factory);
        }


    }
}
