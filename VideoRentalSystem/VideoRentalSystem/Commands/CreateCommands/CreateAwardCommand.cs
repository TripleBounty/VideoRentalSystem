using System;
using System.Collections.Generic;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Data.SqLite.Contracts;
using VideoRentalSystem.Models.Factories;

namespace VideoRentalSystem.Commands.CreateCommands
{
    public class CreateAwardCommand : ICommand
    {
        private readonly IDatabaseLite db;
        private readonly IModelsFactory factory;

        public CreateAwardCommand(IDatabaseLite db, IModelsFactory factory)
        {
            this.db = db;
            this.factory = factory;
        }

        public string Execute(IList<string> parameters)
        {
            if (parameters.Count != 3)
            {
                return "Not valid number of parameters";
            }

            var name = parameters[0];
            var year = parameters[1];
            var orgName = parameters[2];

            var organisation = this.db.Organisations.SingleOrDefault(x => x.Name == orgName);

            if (organisation == null)
            {
                return "organisation not found";
            }

            var award = this.factory.CreateAward(name, year, organisation.Id);
            award.Organisation = organisation;

            this.db.Awards.Add(award);
            this.db.Complete();

            return "Award created";
        }
    }
}
