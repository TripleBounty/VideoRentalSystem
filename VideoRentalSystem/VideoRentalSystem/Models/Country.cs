﻿namespace VideoRentalSystem.Models
{
    public class Country
    {
        public Country(string name, string code)
        {
            this.Name = name;
            this.Code = code;
        }

        public Country()
        {
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public override string ToString()
        {
            return $"{this.Id} {this.Name} {this.Code}";
        }
    }
}