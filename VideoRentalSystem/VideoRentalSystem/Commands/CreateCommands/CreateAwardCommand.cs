using System;
using System.Collections.Generic;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Models.Factories;

namespace VideoRentalSystem.Commands.CreateCommands
{
    class CreateAwardCommand : ICommand
    {
        private readonly IDatabase db;
        private readonly IModelsFactory factory;

        public CreateAwardCommand(IDatabase db, IModelsFactory factory)
        {
            this.db = db;
            this.factory = factory;
        }

        public string Execute(IList<string> parameters)
        {
            var name = parameters[0];
            DateTime date = Convert.ToDateTime(parameters[1]);

            var award = this.factory.CreateAward(name, date);

            this.db.Award.Add(award);
            this.db.Complete();

            return "Award created";
        }
    }
}
