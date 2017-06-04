using System;
using System.Text;

namespace VideoRentalSystem.Models
{
    public class Award
    {
        public Award(string name, DateTime date)
        {
            this.Name = name;
            this.Date = date;

            this.IsDeleted = false;
        }

        public Award()
        {
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        // TODO: organisations
        // public Organisation  Organisation { get; set; }
        public bool IsDeleted { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append("Award: ");
            sb.AppendLine(this.Name);
            sb.Append("Date: ");
            sb.AppendLine(this.Date.ToString());

            return sb.ToString();
        }
    }
}