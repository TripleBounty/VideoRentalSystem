using System.Collections.Generic;
using System.Linq;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Postgre.Contracts;
using VideoRentalSystem.Models.Factories;

namespace VideoRentalSystem.Commands.UpdateCommands
{
    public class UpdateTarifCommand : ICommand
    {
        private readonly IDatabasePostgre db;
        private readonly IModelsFactory factory;

        public UpdateTarifCommand(IDatabasePostgre db, IModelsFactory factory)
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

            int tarifId;
            var tarifIdParsed = int.TryParse(parameters[0], out tarifId);
            if (!tarifIdParsed)
            {
                return "Not Valid Tarif Id. Fill in numeric value!";
            }

            var tarif = this.db.Tarifs.SingleOrDefault(t => t.Id == tarifId);
            if (tarif == null)
            {
                return $"Tarif with such Id doesn't exist!";
            }

            var name = parameters[1];

            decimal price;
            var priceParsed = decimal.TryParse(parameters[2], out price);
            if (!priceParsed)
            {
                return "Not Valid Price. Fill in numeric value!";
            }

            if (tarif.Price != price)
            {
                if (this.db.Tarifs.CheckTarif(tarifId))
                {
                    var tarifNew = this.factory.CreateTarif(name, tarif.MaxNumberOfDays, price);
                    this.db.Tarifs.Add(tarifNew);
                    tarif.IsDeleted = true;
                }
                else
                {
                    tarif.Price = price;
                    tarif.Name = name;
                }
            }
            else
            {
                tarif.Name = name;
            }

            this.db.Complete();

            return "Tarif updated";
        }
    }
}
