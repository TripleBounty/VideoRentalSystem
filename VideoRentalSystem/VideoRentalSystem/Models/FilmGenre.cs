﻿using System.Collections.Generic;

namespace VideoRentalSystem.Models
{
    public class FilmGenre
    {
        public FilmGenre()
        {
            this.Customers = new HashSet<Customer>();
        }

        public FilmGenre(string genre)
        {
            this.Genre = genre;
            this.Customers = new HashSet<Customer>();
        }

        public int Id { get; set; }

        public string Genre { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }

        public bool IsDeleted { get; set; }

        public override string ToString()
        {
            return this.Genre;
        }
    }
}