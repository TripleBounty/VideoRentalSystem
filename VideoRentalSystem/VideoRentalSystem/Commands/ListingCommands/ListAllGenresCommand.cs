using System.Collections.Generic;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;

namespace VideoRentalSystem.Commands.ListingCommands
{
    public class ListAllGenresCommand : ICommand
    {
        private readonly IDatabase db;

        public ListAllGenresCommand(IDatabase db)
        {
            this.db = db;
        }

        public string Execute(IList<string> parameters)
        {
            var genres = this.db.FilmGenres.GetAll();

            if (genres.Count == 0)
            {
                return "No Genres available";
            }

            List<string> genNames = new List<string>();

            foreach (var gen in genres)
            {
                genNames.Add(gen.Genre);
            }

            return string.Join("\n", genNames);
        }
    }
}