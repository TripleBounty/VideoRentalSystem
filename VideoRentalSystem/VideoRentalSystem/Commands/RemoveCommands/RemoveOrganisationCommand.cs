using System;
using System.Collections.Generic;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.SqLite.Contracts;

namespace VideoRentalSystem.Commands.RemoveCommands
{
    public class RemoveOrganisationCommand : ICommand
    {
        private readonly IDatabaseLite db;

        public RemoveOrganisationCommand(IDatabaseLite db)
        {
            if (db == null)
            {
                throw new ArgumentNullException("Invalid database in RemoveOrganisationCommand constructor");
            }

            this.db = db;
        }

        public string Execute(IList<string> parameters)
        {
            var orgName = parameters[0];
            var organisation = this.db.Organisations.SingleOrDefault(x => x.Name == orgName);

            if (organisation == null)
            {
                return "organisation not found";
            }

            organisation.IsDeleted = true;
            this.db.Complete();

            return "organisation removed";
        }
    }
}
