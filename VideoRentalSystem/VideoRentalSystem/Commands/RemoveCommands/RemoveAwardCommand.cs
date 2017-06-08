using System;
using System.Collections.Generic;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Models.Factories;

namespace VideoRentalSystem.Commands.RemoveCommands
{
    public class RemoveAwardCommand : ICommand
    {
        private IDatabase db;
        private IModelsFactory factory;

        public RemoveAwardCommand(IDatabase data, IModelsFactory factory)
        {
            this.db = data;
            this.factory = factory;
        }

        public string Execute(IList<string> parameters)
        {
            //var awardName = parameters[0];
            //var award = this.db.Award.SingleOrDefault(x => x.Name == awardName);

            //if (award == null)
            //{
            //    return "Award not found";
            //}

            //var id = award.Id;
            //award.IsDeleted = true;

            //this.db.Complete();

            //return "Award removed";

            throw new NotImplementedException();
        }
    }
}
