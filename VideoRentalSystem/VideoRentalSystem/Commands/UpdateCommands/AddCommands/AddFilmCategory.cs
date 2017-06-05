using System.Collections.Generic;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Models.Factories;
using VideoRentalSystem.Models.Enum;
using System;

namespace VideoRentalSystem.Commands.UpdateCommands.AddCommands
{
    public class AddFilmCategory : ICommand
    {
        private readonly IDatabase db;
        private readonly IModelsFactory factory;

        public AddFilmCategory(IDatabase db, IModelsFactory factory)
        {
            this.db = db;
            this.factory = factory;
        }

        public string Execute(IList<string> parameters)
        {
            var filmName = parameters[0];
            var filmCategory = (MPAA_Rating) Enum.Parse(typeof(MPAA_Rating), parameters[1].ToUpper());

            db.Film.SingleOrDefault(x => x.Name == filmName).Categories.Add(filmCategory);

            this.db.Complete();

            return "MPAA rationg added to " + filmName;
        }
    }
}
