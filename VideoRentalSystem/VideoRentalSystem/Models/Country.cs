using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}