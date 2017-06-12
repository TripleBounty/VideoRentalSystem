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
        private readonly IDatabaseLite lite;
        //private readonly IDatabase db;
        private readonly IModelsFactory factory;

        public CreateAwardCommand(IDatabaseLite lite, IModelsFactory factory)
        {
            this.lite = lite;
            //this.db = db;
            this.factory = factory;
        }

        public string Execute(IList<string> parameters)
        {
            if (parameters.Count != 3)
            {
                return "Not valid number of parameters";
            }

            var name = parameters[0];

            var search = this.lite.Awards.SingleOrDefault(x => x.Name == name);

            if (search != null)
            {
                return "award allready exists";
            }

            var year = parameters[1];
            var orgName = parameters[2];

            var organisation = this.lite.Organisations.SingleOrDefault(x => x.Name == orgName);

            if (organisation == null)
            {
                return "organisation not found";
            }

            var award = this.factory.CreateAward(name, year, organisation.Id);
            award.Organisation = organisation;

            this.lite.Awards.Add(award);
            this.lite.Complete();

            return "Award created";
        }
    }
}
