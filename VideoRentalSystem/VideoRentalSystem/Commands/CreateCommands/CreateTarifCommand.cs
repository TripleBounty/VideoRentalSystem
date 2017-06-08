using System.Collections.Generic;
using System.Linq;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Postgre.Contracts;
using VideoRentalSystem.Models.Factories;

namespace VideoRentalSystem.Commands.CreateCommands
{
    public class CreateTarifCommand : ICommand
    {
        private readonly IDatabasePostgre db;
        private readonly IModelsFactory factory;

        public CreateTarifCommand(IDatabasePostgre db, IModelsFactory factory)
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

            if (parameters.Any(x => x == string.Empty))
            {
                return "Some of the passed parameters are empty!";
            }

            var name = parameters[0];
            int maxNumberOfDays;
            var maxNumberOfDaysParsed = int.TryParse(parameters[1], out maxNumberOfDays);
            if (!maxNumberOfDaysParsed)
            {
                return "Not Valid Day parameter. Fill in numeric value!";
            }

            decimal price;
            var priceParsed = decimal.TryParse(parameters[2], out price);
            if (!priceParsed)
            {
                return "Not Valid Price. Fill in numeric value!";
            }

            var tarif = this.db.Tarifs.SingleOrDefault(t => t.MaxNumberOfDays == maxNumberOfDays && t.IsDeleted == false);
            if (tarif != null)
            {
                return $"Tarif with such max number of days already exist!\n{tarif.ToString()}";
            }

            tarif = this.factory.CreateTarif(name, maxNumberOfDays, price);

            this.db.Tarifs.Add(tarif);
            this.db.Complete();

            return "Tarif created";
        }
    }
}
