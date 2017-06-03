using System;

namespace VideoRentalSystem.Models
{
    public class Award
    {
        private Award()
        {
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        // TODO: organisations
        // public Organisation  Organisation { get; set; }
    }
}