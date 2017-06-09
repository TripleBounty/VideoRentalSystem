using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Models.Factories;

namespace VideoRentalSystem.Commands.UpdateCommands
{
    public class UpdateAwardCommand : ICommand
    {
        private IDatabase db;
        private IModelsFactory factory;

        public UpdateAwardCommand(IDatabase db, IModelsFactory factory)
        {
            this.db = db;
            this.factory = factory;
        }

        public string Execute(IList<string> parameters)
        {
            ////if (parameters.Count != 3)
            ////{
            ////    return "Not valid number of parameters";
            ////}

            throw new NotImplementedException();

            ////return "";
        }
    }
}
