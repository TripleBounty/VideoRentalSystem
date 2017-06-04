using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoRentalSystem.Models
{
    public class Address
    {
        public Address(string street, string postalCode, Town town, Country country)
        {
            this.Street = street;
            this.PostalCode = postalCode;
            this.Town = town;
            this.Country = country;
        }

        private Address()
        {
        }

        public int Id { get; set; }

        public string PostalCode { get; set; }

        public string Street { get; set; }

        public virtual Town Town { get; set; }

        public virtual Country Country { get; set; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append("Street: ");
            stringBuilder.AppendLine(this.Street);
            stringBuilder.Append("Postal Code: ");
            stringBuilder.AppendLine(this.PostalCode);
            stringBuilder.Append(this.Town.ToString());

            return stringBuilder.ToString();
        }
    }
}
