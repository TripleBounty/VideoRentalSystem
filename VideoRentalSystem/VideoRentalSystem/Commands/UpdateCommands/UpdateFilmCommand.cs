using System;
using System.Collections.Generic;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Models.Factories;

namespace VideoRentalSystem.Commands.UpdateCommands
{
    public class UpdateFilmCommand : ICommand
    {
        private IDatabase db;
        private IModelsFactory factory;

        public UpdateFilmCommand(IDatabase db, IModelsFactory factory)
        {
            this.db = db;
            this.factory = factory;
        }

        public string Execute(IList<string> parameters)
        {
            if (parameters.Count != 5)
            {
                return "Not valid number of parameters";
            }

            var filmId = int.Parse(parameters[0]);
            var film = this.db.Films.SingleOrDefault(x => x.Id == filmId);

            if (film == null)
            {
                return "film not found";
            }

            var filmName = parameters[1];
            var newSummary = parameters[2];
            var newReleaseDate = parameters[3];
            var newDuration = parameters[4];

            film.Name = filmName;
            film.Summary = newSummary;
            film.ReleaseDate = Convert.ToDateTime(newReleaseDate);
            film.Duration = TimeSpan.FromMinutes(double.Parse(newDuration));

            this.db.Complete();

            return filmName + " updated";
        }
    }
}
