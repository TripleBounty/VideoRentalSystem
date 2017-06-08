using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.SqLite.Contracts;
using VideoRentalSystem.Models.Factories;

namespace VideoRentalSystem.Commands.CreateCommands
{
    public class CreateOrganisationCommand : ICommand
    {
        private readonly IDatabaseLite db;
        private readonly IModelsFactory factory;

        public CreateOrganisationCommand(IDatabaseLite db, IModelsFactory factory)
        {
            this.db = db;
            this.factory = factory;
        }

        public string Execute(IList<string> parameters)
        {
            if (parameters.Count != 1)
            {
                return "no name provided";
            }

            var name = parameters[0];

            var org = this.factory.CreateOrganisation(name);

            db.Organisations.Add(org);
            db.Complete();

            return name + " created";
        }
    }
}
