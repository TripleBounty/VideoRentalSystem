using System;
using System.Collections.Generic;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data;
using VideoRentalSystem.Models.Factories;

namespace VideoRentalSystem.Commands
{
    public class CreateUserCommand : ICommand
    {
        public CreateUserCommand(IDatabase db, IModelsFactory factory)
        {

        }

        public string Execute(IList<string> parameters)
        {
            throw new NotImplementedException();
        }
    }
}
