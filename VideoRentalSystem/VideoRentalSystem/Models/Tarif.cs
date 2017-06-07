﻿using System;

namespace VideoRentalSystem.Models
{
    public class Tarif
    {
        public Tarif(string name, int maxNumberOfDays, decimal price)
        {
            this.Name = name;
            this.MaxNumberOfDays = maxNumberOfDays;
            this.Price = price;
        }

        public Tarif()
        {
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int MaxNumberOfDays { get; set; }

        public decimal Price { get; set; }

        public override string ToString()
        {
            return $"{this.Id}. {this.Name} Max days on this tarif: {this.MaxNumberOfDays} Price: {this.Price}";
        }
    }
}
