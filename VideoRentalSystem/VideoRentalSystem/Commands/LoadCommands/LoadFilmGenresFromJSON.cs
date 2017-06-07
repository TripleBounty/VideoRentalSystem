using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Models.Factories;

namespace VideoRentalSystem.Commands.CreateCommands
{
    public class LoadFilmGenresFromJSONCommand : ICommand
    {
        private readonly IDatabase db;
        private readonly IModelsFactory factory;

        public LoadFilmGenresFromJSONCommand(IDatabase db, IModelsFactory factory)
        {
            this.db = db;
            this.factory = factory;
        }

        public string Execute(IList<string> parameters)
        {
            string fileLoc = parameters[0];
            using (StreamReader r = new StreamReader(fileLoc))
            {
                string json = r.ReadToEnd();
                dynamic data = JObject.Parse(json);
                foreach (var genresList in data.genre)
                {
                    foreach (var currentGenre in genresList)
                    {
                        string genre = currentGenre.Genre;                      

                        var filmGenre = this.factory.CreateFilmGenre(genre);

                        this.db.FilmGenres.Add(filmGenre);
                        this.db.Complete();
                    }
                }
            }

            return "FilmGenres created";
        }
    }
}