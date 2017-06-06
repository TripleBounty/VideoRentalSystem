using System.Text;

namespace VideoRentalSystem.Models
{
    public class Address
    {
        public Address(string street, string postalCode, Town town)
        {
            this.Street = street;
            this.PostalCode = postalCode;
            this.Town = town;
        }

        public Address()
        {
        }

        public int Id { get; set; }

        public string PostalCode { get; set; }

        public string Street { get; set; }

        public virtual Town Town { get; set; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"Street: {this.Street}");
            stringBuilder.AppendLine($"Postal Code: {this.PostalCode}");
            stringBuilder.Append(this.Town.ToString());

            return stringBuilder.ToString();
        }
    }
}
