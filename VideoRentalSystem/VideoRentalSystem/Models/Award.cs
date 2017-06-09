using System;
using System.Collections.Generic;
using System.Text;

namespace VideoRentalSystem
{
    public class Award
    {
        public Award()
        {
            this.IsDeleted = false;
        }

        public Award(string name, string year, long orgId)
        {
            this.Name = name;
            this.Year = year;
            this.OrganisationId = orgId;
            this.IsDeleted = false;
        }

        public long Id { get; set; }

        public string Name { get; set; }

        public string Year { get; set; }

        public bool IsDeleted { get; set; }

        public long OrganisationId { get; set; }

        public virtual Organisation Organisation { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append("Award: ");
            sb.AppendLine(this.Name);
            sb.Append("Date: ");
            sb.AppendLine(this.Year.ToString());

            return sb.ToString();
        }
    }
}
