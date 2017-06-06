using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoRentalSystem.Models.Enum;

namespace VideoRentalSystem.Models
{
    public class Storage
    {
        public Storage(Store store, Film film, int quantity, VideoFormat videoFormat)
        {
            this.Store = store;
            this.Film = film;
            this.Quantity = quantity;
            this.VideoFormat = videoFormat;
        }

        public Storage()
        {
        }

        public int Id { get; set; }

        public Store Store { get; set; }

        public Film Film { get; set; }

        public int Quantity { get; set; }

        public VideoFormat VideoFormat { get; set; }

        public override string ToString()
        {
            return $"Storage: {this.Store.Id} Film: {this.Film.Id} Format: {this.VideoFormat} Quantity: {this.Quantity}";
        }
    }
}
