﻿using System;
using System.Collections.Generic;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;
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

        // TODO: Refacotor this code Jmitko
        public string Execute(IList<string> parameters)
        {
            var filmName = parameters[0];
            var summary = parameters[1];
            DateTime realiseDate = Convert.ToDateTime(parameters[2]);
            TimeSpan duration = TimeSpan.FromMinutes(double.Parse(parameters[3]));

            var film = this.factory.CreateFilm(filmName, summary, realiseDate, duration);

            this.db.Films.Add(film);
            this.db.Complete();

            return "Film created";
        }
    }
}
