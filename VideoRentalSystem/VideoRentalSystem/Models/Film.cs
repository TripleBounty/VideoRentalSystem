using System;
using System.Collections.Generic;
using VideoRentalSystem.Models.Enum;

namespace VideoRentalSystem.Models
{
    public class Film
    { 
        private Film()
        {
            this.Categories = new HashSet<MPAA_Rating>();
            this.Genres = new HashSet<Genre>();
            this.Awards = new HashSet<Award>();
        }

        public Film(string name, string summary, DateTime realiseDate, TimeSpan duration, VideoFormat format, int count, float rating)
        {
            this.Name = name;
            this.Summary = summary;
            this.RealieseDate = realiseDate;
            this.Duration = duration;
            this.VideoFormats = format;
            this.InStore = count;
            this.Rating = rating;

            this.Categories = new HashSet<MPAA_Rating>();
            this.Genres = new HashSet<Genre>();
            this.Awards = new HashSet<Award>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Summary { get; set; }

        public DateTime RealieseDate { get; set; }

        public TimeSpan Duration { get; set; }

        public virtual ICollection<MPAA_Rating> Categories { get; set; }

        public virtual ICollection<Genre> Genres { get; set; }

        public VideoFormat VideoFormats { get; set; }

        public int InStore { get; set; }

        // TODO: director, write, actor
        // public ICollection<Director> Directors { get; set; }

        // public ICollection<Writer> Writers { get; set; }

        // public ICollection<Actor> Actors { get; set; }
        public float Rating { get; set; }

        public virtual ICollection<Award> Awards { get; set; }
    }
}
