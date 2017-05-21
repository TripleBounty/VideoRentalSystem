using System;
using System.Linq;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Common.Contracts;

namespace ProjectManager.Common.Providers
{
    public class CommandProcessor : IProcessor
    {

        private ICommandsFactory factory;

        public CommandProcessor(ICommandsFactory factory)
        {
            this.factory = factory;
        }

        public string ProcessCommand(string commandLine)
        {
            if (string.IsNullOrWhiteSpace(commandLine))
            {
                throw new Exception("No command has been provided!");
            }

            var commandName = commandLine.Split(' ')[0];
            var commandParameters = commandLine
                .Split(' ')
                .Skip(1)
                .ToList();

            var command = this.factory.CreateCommandFromString(commandName);
            var executionResult = command.Execute(commandParameters);

            return executionResult;
        }
    }
}