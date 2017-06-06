using System;

namespace VideoRentalSystem.Models
{
    public class Loan
    {
        public Loan(int storeId, int filmId, int customerId, DateTime startDate)
        {
            this.StoreId = storeId;
            this.FilmId = filmId;
            this.CustomerId = customerId;
            this.StartDate = startDate;
        }

        public Loan()
        {
        }

        public int Id { get; set; }

        public int StoreId { get; set; }

        public int FilmId { get; set; }

        public int CustomerId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public Tarif Tarif { get; set; }
    }
}
