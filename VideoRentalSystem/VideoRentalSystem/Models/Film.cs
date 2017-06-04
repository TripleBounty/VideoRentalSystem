using System;
using System.Collections.Generic;
using System.Text;
using VideoRentalSystem.Models.Enum;

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

            this.Categories = new HashSet<MPAA_Rating>();
            this.Genres = new HashSet<Genre>();
            this.Awards = new HashSet<Award>();
            this.Stores = new HashSet<Store>();
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

        public virtual ICollection<Store> Stores { get; set; }

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
            sb.AppendLine(string.Join(",", this.Categories));
            sb.Append("Genres: ");
            sb.AppendLine(string.Join(",", this.Genres));
            sb.Append("Awards: ");
            sb.AppendLine(string.Join(",", this.Awards));
            //// TODO FilmStaff

            return sb.ToString();
        }
    }
}
