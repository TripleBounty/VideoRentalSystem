using System;
using System.Collections.Generic;
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
            this.db = db;
            this.factory = factory;
        }

        public string Execute(IList<string> parameters)
        {

            var firstName = parameters[0];
            var lastName = parameters[1];
            DateTime birthDate = Convert.ToDateTime(parameters[2]);
            var countryId = int.Parse(parameters[3]);

            StaffType type = (StaffType)Enum.Parse(typeof(StaffType), parameters[4], true);

            var country = this.db.Countries.SingleOrDefault(c => c.Id == countryId);

            var filmStaff = this.factory.CreateFilmStaff(firstName, lastName, birthDate, country, type);

            this.db.FilmStaffs.Add(filmStaff);
            this.db.Complete();

            return "filmStaff created";
        }
    }
}
