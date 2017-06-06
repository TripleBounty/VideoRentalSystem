using System;
using System.Text;
using VideoRentalSystem.Common;

namespace VideoRentalSystem.Models
{
    public class Loan
    {
        public Loan(int storeId, int filmId, int customerId)
        {
            this.StoreId = storeId;
            this.FilmId = filmId;
            this.CustomerId = customerId;
            this.StartDate = TimeProvider.Current.UtcNow;
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
