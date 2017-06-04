using System.Text;

namespace VideoRentalSystem.Models
{
    public class Town
    {
        public Town(string name, Country country)
        {
            this.Name = name;
            this.Country = country;
        }

        public Town()
        {
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual Country Country { get; set; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append("Town: ");
            stringBuilder.AppendLine(this.Name);
            stringBuilder.Append("Country: ");
            stringBuilder.Append(this.Country.ToString());

            return stringBuilder.ToString();
        }
    }
}
