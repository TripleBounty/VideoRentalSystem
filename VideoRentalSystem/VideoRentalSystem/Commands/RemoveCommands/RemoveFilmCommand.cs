using System.Collections.Generic;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;

namespace VideoRentalSystem.Commands.RemoveCommands
{
    public class RemoveFilmCommand : ICommand
    {
        private readonly IDatabase db;
        
        public RemoveFilmCommand(IDatabase db)
        {
            this.db = db;
        }

        public string Execute(IList<string> parameters)
        {
            var filmName = parameters[0];

            var film = this.db.Films.SingleOrDefault(x => x.Name == filmName);

            if (film == null)
            {
                return "film not found";
            }

            film.IsDeleted = true;
            this.db.Complete();

            return "Film removed";
        }
    }
}
