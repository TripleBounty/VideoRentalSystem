using System.Collections.Generic;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.SqLite.Contracts;

namespace VideoRentalSystem.Commands.UpdateCommands
{
    public class UpdateAwardCommand : ICommand
    {
        private readonly IDatabaseLite db;
        
        public UpdateAwardCommand(IDatabaseLite db)
        {
            this.db = db;
        }

        public string Execute(IList<string> parameters)
        {
            if (parameters.Count != 4)
            {
                return "Not valid number of parameters";
            }

            var awardId = int.Parse(parameters[0]);
            var award = this.db.Awards.SingleOrDefault(x => x.Id == awardId);

            if (award == null)
            {
                return "Award not found";
            }

            var orgId = long.Parse(parameters[3]);
            var org = this.db.Organisations.SingleOrDefault(x => x.Id == orgId);

            if (org == null)
            {
                return "organisaation not found";
            }

            award.Name = parameters[1];
            award.Year = parameters[2];
            award.OrganisationId = orgId;
            award.Organisation = org;
            this.db.Complete();

            return "Award updated";
        }
    }
}
