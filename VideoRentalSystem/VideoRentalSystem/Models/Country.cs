﻿using System.Text;
using VideoRentalSystem.Models.Contracts;

namespace VideoRentalSystem.Models
{
    public class Country : ICountry
    {
        private Country()
        {

        }

        public Country(string name, string code)
        {
            this.Name = name;
            this.Code = code;
        }

        public Country(int id, string name, string code)
            : this(name, code)
        {
            this.Id = id;
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