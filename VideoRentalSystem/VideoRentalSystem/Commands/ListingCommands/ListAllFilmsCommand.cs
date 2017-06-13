using System.Collections.Generic;
using System.Linq;
using System.Text;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Common.Contracts;
using VideoRentalSystem.Data.Contracts;

namespace VideoRentalSystem.Commands.ListingCommands
{
    public class ListAllFilmsCommand : ICommand
    {
        private readonly IDatabase db;
        private readonly IGetFilmScore score;

        public ListAllFilmsCommand(IDatabase db, IGetFilmScore score)
        {
            this.db = db;
            this.score = score;
        }

        public string Execute(IList<string> parameters)
        {
            var films = (from film in this.db.Films.GetAll()
                         where film.IsDeleted == false
                         select film).ToList();

            if (films.Count == 0)
            {
                return "No Films available";
            }

            var sb = new StringBuilder();

            foreach (var f in films)
            {
                sb.Append(f.ToString());
                sb.Append("Avg Score: ");
                sb.AppendLine(this.score.GetAvgFilmScore(f.Name).ToString());
            }

            return sb.ToString();
        }
    }
}