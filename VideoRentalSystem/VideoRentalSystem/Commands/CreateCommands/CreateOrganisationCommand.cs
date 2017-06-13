using System.Collections.Generic;
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

            var search = this.db.Organisations.SingleOrDefault(x => x.Name == name);

            if (search != null)
            {
                return "organisation allready exists";
            }
            else
            {
                var org = this.factory.CreateOrganisation(name);

                this.db.Organisations.Add(org);
                this.db.Complete();

                return name + " created";
            }           
        }
    }
}
