using System;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Core.Contracts;
using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Models.Factories;

namespace VideoRentalSystem.Commands.Factory
{
    public class CommandsFactory : ICommandsFactory
    {
        private readonly IServiceLocator serviceLocator;

        public CommandsFactory(IServiceLocator serviceLocator)
        {
            this.serviceLocator = serviceLocator;
        }

        public ICommand CreateCommandFromString(string commandName)
        {
            //       throw new Exception("The passed command is not valid!");
            return this.serviceLocator.GetCommand(commandName);
           
        }
    }
}
