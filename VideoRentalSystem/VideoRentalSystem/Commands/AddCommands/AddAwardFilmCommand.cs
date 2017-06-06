using System;
using System.Collections.Generic;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;

namespace VideoRentalSystem.Commands.AddCommands
{
    public class AddAwardFilmCommand : ICommand
    {
        private readonly IDatabase db;

        public AddAwardFilmCommand(IDatabase db)
        {
            this.db = db;
        }

        // TODO: validations
        public string Execute(IList<string> parameters)
        {
            if (parameters.Count != 2)
            {
                return "Not valid number of parameters";
            }

            var filmName = parameters[0];
            var film = this.db.Films.SingleOrDefault(x => x.Name == filmName);
            if (film == null)
            {
                return "film not found";
            }

            var awardName = parameters[1];
            var award = this.db.Award.SingleOrDefault(x => x.Name == awardName);
            if (award == null)
            {
                return "award not found";
            }

            film.Awards.Add(award);
            this.db.Complete();

            return awardName + " added to " + filmName;
        }
    }
}
