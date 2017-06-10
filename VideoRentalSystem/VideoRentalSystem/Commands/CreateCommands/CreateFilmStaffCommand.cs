using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Models.Enum;
using VideoRentalSystem.Models.Factories;

namespace VideoRentalSystem.Commands.CreateCommands
{
    public class CreateFilmStaffCommand : ICommand
    {
        private readonly IDatabase db;
        private readonly IModelsFactory factory;

        public CreateFilmStaffCommand(IDatabase db, IModelsFactory factory)
        {
            if (db == null)
            {
                throw new ArgumentNullException("CreateFilmStaffCommand db parameter cannot be null");
            }

            this.db = db;

            if (factory == null)
            {
                throw new ArgumentNullException("CreateFilmStaffCommand factory parameter cannot be null");
            }

            this.factory = factory;
        }

        public string Execute(IList<string> parameters)
        {
            if (parameters.Count != 5)
            {
                return "Not valid number of parameters";
            }

            if (parameters.Any(x => x == string.Empty))
            {
                return "Some of the passed parameters are empty!";
            }

            var firstName = parameters[0];
            var lastName = parameters[1];

            DateTime birthDate = Convert.ToDateTime(parameters[2]);

            int countryId;
            var countryIdParsed = int.TryParse(parameters[3], out countryId);
            if (!countryIdParsed)
            {
                return "Not Valid Country Id. Fill in numeric value!";
            }

            var country = this.db.Countries.SingleOrDefault(c => c.Id == countryId);
            if (country == null)
            {
                return "Country with such id doesn't exist!";
            }

            StaffType type;
            var typeParsed = Enum.TryParse<StaffType>(parameters[4], true, out type);
            if (!typeParsed)
            {
                return "Not Valid Film Staff type!";
            }

            var filmStaff = this.factory.CreateFilmStaff(firstName, lastName, birthDate, country, type);

            this.db.FilmStaffs.Add(filmStaff);
            this.db.Complete();

            return "filmStaff created";
        }
    }
}
