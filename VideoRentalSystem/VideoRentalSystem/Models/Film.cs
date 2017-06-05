using System;
using System.Collections.Generic;
using System.Text;

namespace VideoRentalSystem.Models
{
    public class Film
    { 
        public Film(string name, string summary, DateTime releaseDate, TimeSpan duration)
        {
            this.Name = name;
            this.Summary = summary;
            this.ReleaseDate = releaseDate;
            this.Duration = duration;

            this.IsDeleted = false;

            this.Categories = new HashSet<FilmRating>();
            this.Genres = new HashSet<FilmGenre>();
            this.Awards = new HashSet<Award>();
            this.FilmStaffs = new HashSet<FilmStaff>();
        }

        public Film()
        {
            this.Categories = new HashSet<FilmRating>();
            this.Genres = new HashSet<FilmGenre>();
            this.Awards = new HashSet<Award>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Summary { get; set; }

        public DateTime ReleaseDate { get; set; }

        public TimeSpan Duration { get; set; }

        public virtual ICollection<FilmRating> Categories { get; set; }

        public virtual ICollection<FilmGenre> Genres { get; set; }

        public virtual ICollection<Award> Awards { get; set; }

        public virtual ICollection<FilmStaff> FilmStaffs { get; set; }

        public bool IsDeleted { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append("Film: ");
            sb.AppendLine(this.Name);
            sb.Append("Release date: ");
            sb.AppendLine(this.ReleaseDate.ToString());
            sb.Append("Duration: ");
            sb.AppendLine(this.Duration.TotalMinutes.ToString());
            sb.Append("Rating: ");
            sb.AppendLine(string.Join(",", this.Categories.ToString()));
            sb.Append("Genres: ");
            sb.AppendLine(string.Join(",", this.Genres.ToString()));
            sb.Append("Awards: ");
            sb.AppendLine(string.Join(",", this.Awards.ToString()));
            //// TODO FilmStaff

            return sb.ToString();
        }
    }
}
