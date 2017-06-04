using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoRentalSystem.Models.Enum;

namespace VideoRentalSystem.Models
{
    public class FilmStaff
    {
        public FilmStaff(string firstName, string lastName, DateTime birthDate, Country originePlace, StaffType type)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.BirthDate = birthDate;
            this.OriginePlace = originePlace;
            this.Type = type;
            this.Films = new HashSet<Film>();
        }

        public FilmStaff()
        {
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public virtual Country OriginePlace { get; set; }

        public virtual StaffType Type { get; set; }

        public virtual ICollection<Film> Films { get; set; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"{this.FirstName} {this.LastName} {this.BirthDate}");
            stringBuilder.AppendLine($"Type: {this.Type}");
            stringBuilder.AppendLine($"Origine: {this.OriginePlace}");

            return stringBuilder.ToString();
        }
    }
}
