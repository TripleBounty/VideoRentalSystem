using System;
using System.Collections.Generic;

using VideoRentalSystem.Models.Enum;

namespace VideoRentalSystem.Models.Contracts
{
    public interface IFilm
    {
        int Id { get; set; }

        string Name { get; set; }

        DateTime ReleaseDate { get; set; }

        IList<string> Directors { get; set; }

        IList<string> Writers { get; set; }

        IList<string> Actors { get; set; }

        IList<Genre> Genre { get; set; }

        double Rating { get; set; }

        IList<IAward> Awards { get; set; }
    }
}