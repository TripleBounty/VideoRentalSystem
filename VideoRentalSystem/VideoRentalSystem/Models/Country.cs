using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoRentalSystem.Models
{
    public class Country
    {
        [Key]
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
    }
}