using System.ComponentModel.DataAnnotations;
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

        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }
    }
}