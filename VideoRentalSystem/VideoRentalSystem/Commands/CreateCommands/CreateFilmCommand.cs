using System;
using System.Collections.Generic;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Models.Enum;
using VideoRentalSystem.Models.Factories;

namespace VideoRentalSystem.Commands.CreateCommands
{
    public class CreateFilmCommand : ICommand
    {
        private readonly IDatabase db;
        private readonly IModelsFactory factory;

        public CreateFilmCommand(IDatabase db, IModelsFactory factory)
        {
            this.db = db;
            this.factory = factory;
        }

        // TODO: don't leave me like this
        public string Execute(IList<string> parameters)
        {
            var filmName = parameters[0];
            var summary = parameters[1];
            DateTime realiseDate = Convert.ToDateTime(parameters[2]);
            TimeSpan duration = TimeSpan.FromMinutes(double.Parse(parameters[3]));
            VideoFormat videoFormat = (VideoFormat)Enum.Parse(typeof(VideoFormat), parameters[4], true);

            // TODO: tryParse
            var inStoreCount = int.Parse(parameters[5]);
            var ration = float.Parse(parameters[6]);

            var film = this.factory.CreateFilm(filmName, summary, realiseDate, duration, videoFormat, inStoreCount, ration);

            this.db.Film.Add(film);
            this.db.Complete();

            return "Film created";
        }
    }
}
