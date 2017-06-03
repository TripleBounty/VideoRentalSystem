using VideoRentalSystem.Models.Contracts;

namespace VideoRentalSystem.Models
{
    public class Country : ICountry
    {
        public Country(string name, string code)
        {
            this.Name = name;
            this.Code = code;
        }

        private Country()
        {
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", this.Id, this.Name, this.Code);
        }
    }
}