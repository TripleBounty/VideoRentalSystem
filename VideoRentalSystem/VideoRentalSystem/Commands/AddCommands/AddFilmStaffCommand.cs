using System.Collections.Generic;
using System.Linq;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Models.Factories;

namespace VideoRentalSystem.Commands.AddCommands
{
    public class AddFilmStaffCommand : ICommand
    {
        private readonly IDatabase db;

        public AddFilmStaffCommand(IDatabase db)
        {
            this.db = db;
        }

        public string Execute(IList<string> parameters)
        {
            if (parameters.Count != 2)
            {
                return "Not valid number of parameters";
            }

            if (parameters.Any(x => x == string.Empty))
            {
                return "Some of the passed parameters are empty!";
            }

            int filmId;
            var filmIdParsed = int.TryParse(parameters[0], out filmId);
            if (!filmIdParsed)
            {
                return "Not Valid Film Id. Fill in numeric value!";
            }

            var film = this.db.Films.SingleOrDefault(f => f.Id == filmId);
            if (film == null)
            {
                return "Film with such id doesn't exist!";
            }

            int staffId;
            var staffIdParsed = int.TryParse(parameters[1], out staffId);
            if (!staffIdParsed)
            {
                return "Not Valid Staff Id. Fill in numeric value!";
            }

            var staff = this.db.FilmStaffs.SingleOrDefault(s => s.Id == staffId);
            if (staff == null)
            {
                return "Staff with such id doesn't exist!";
            }

            if (staff.Films.Any(f => f.Id == filmId))
            {
                return $"Film is already assigned";
            }

            staff.Films.Add(film);

            this.db.Complete();

            return "Film added";
        }
    }
}
