using System.Collections.Generic;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;

namespace VideoRentalSystem.Commands.ListingCommands
{
    public class ListAllFilmStaffsCommand : ICommand
    {
        private readonly IDatabase db;

        public ListAllFilmStaffsCommand(IDatabase db)
        {
            this.db = db;
        }

        public string Execute(IList<string> parameters)
        {
            var filmStaffs = this.db.FilmStaffs.GetAll();

            return string.Join("\n", filmStaffs);
        }
    }
}
