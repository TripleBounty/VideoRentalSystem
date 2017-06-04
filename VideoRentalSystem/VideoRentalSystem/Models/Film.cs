using System;
using System.Collections.Generic;
using VideoRentalSystem.Models.Enum;

namespace VideoRentalSystem.Models
{
    public class Film
    {
        public Film(string name, string summary, DateTime releaseDate, TimeSpan duration, VideoFormat format)
        {
            this.Name = name;
            this.Summary = summary;
            this.ReleaseDate = releaseDate;
            this.Duration = duration;
            this.VideoFormats = format;

            this.IsDeleted = false;

            this.Categories = new HashSet<MPAA_Rating>();
            this.Genres = new HashSet<Genre>();
            this.Awards = new HashSet<Award>();
            this.FilmStaffs = new HashSet<FilmStaff>();
        }

        public Film()
        {
            this.Categories = new HashSet<MPAA_Rating>();
            this.Genres = new HashSet<Genre>();
            this.Awards = new HashSet<Award>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Summary { get; set; }

        public DateTime ReleaseDate { get; set; }

        public TimeSpan Duration { get; set; }

        public virtual ICollection<MPAA_Rating> Categories { get; set; }

        public virtual ICollection<Genre> Genres { get; set; }

        public VideoFormat VideoFormats { get; set; }

        // TODO: director, write, actor
        // public ICollection<Director> Directors { get; set; }

        // public ICollection<Writer> Writers { get; set; }

        // public ICollection<Actor> Actors { get; set; }
        public virtual ICollection<Award> Awards { get; set; }

        public virtual ICollection<FilmStaff> FilmStaffs { get; set; }

        public bool IsDeleted { get; set; }
    }
}
