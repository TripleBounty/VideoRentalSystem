using System;

namespace VideoRentalSystem.Models
{
    public class Tarif
    {
        public Tarif(string name, int maxNumberOfDays, decimal price)
        {
            this.Name = name;
            this.MaxNumberOfDays = maxNumberOfDays;
            this.Price = price;
            this.IsDeleted = false;
        }

        public Tarif()
        {
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int MaxNumberOfDays { get; set; }

        public decimal Price { get; set; }

        public bool IsDeleted { get; set; }

        public override string ToString()
        {
            var active = this.IsDeleted ? "No" : "Yes";
            return $"{this.Id}. {this.Name} Max days on this tarif: {this.MaxNumberOfDays} Price per day: {this.Price:C2} Active:{active}";
        }
    }
}
