using System;
using System.Collections.Generic;
using System.Linq;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Postgre.Contracts;

namespace VideoRentalSystem.Commands.RemoveCommands
{
    public class RemoveTarifCommand : ICommand
    {
        private readonly IDatabasePostgre db;

        public RemoveTarifCommand(IDatabasePostgre db)
        {
            if (db == null)
            {
                throw new ArgumentNullException("db parameter in RemoveTarifCommand Command cannot be null");
            }

            this.db = db;
        }

        public string Execute(IList<string> parameters)
        {
            if (parameters.Count != 1)
            {
                return "Not valid number of parameters";
            }

            if (parameters.Any(x => x == string.Empty))
            {
                return "Some of the passed parameters are empty!";
            }

            int tarifId;
            var tarifIdParsed = int.TryParse(parameters[0], out tarifId);
            if (!tarifIdParsed)
            {
                return "Not Valid Tarif Id. Fill in numeric value!";
            }

            var tarif = this.db.Tarifs.SingleOrDefault(t => t.Id == tarifId);
            if (tarif == null)
            {
                return "Tarif with such id doesn't exist!";
            }

            tarif.IsDeleted = true;

            this.db.Complete();

            return "Tarif updated";
        }
    }
}
