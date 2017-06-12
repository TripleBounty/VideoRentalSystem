using System;
using System.Collections.Generic;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.SqLite.Contracts;

namespace VideoRentalSystem.Commands.RemoveCommands
{
    public class RemoveAwardCommand : ICommand
    {
        private readonly IDatabaseLite db;

        public RemoveAwardCommand(IDatabaseLite data)
        {
            this.db = data;
        }

        public string Execute(IList<string> parameters)
        {
            var awardName = parameters[0];
            var award = this.db.Awards.SingleOrDefault(x => x.Name == awardName);

            if (award == null)
            {
                return "Award not found";
            }

            award.IsDeleted = true;
            this.db.Complete();

            return "Award removed";
        }
    }
}
