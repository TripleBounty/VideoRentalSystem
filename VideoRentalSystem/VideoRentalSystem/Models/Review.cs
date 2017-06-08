using System.Text;

namespace VideoRentalSystem.Models
{
    public class Review
    {
        public Review(double rating, string description, Film film, Customer customer)
        {
            this.Film = film;
            this.Customer = customer;
            this.Rating = rating;
            this.Description = description;
        }

        public Review()
        {
        }

        public int Id { get; set; }

        public virtual Film Film { get; set; }

        public virtual Customer Customer { get; set; }

        public double Rating { get; set; }

        public string Description { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append("Review for the movie: ");
            sb.AppendLine(this.Film.Name.ToString());
            sb.Append("Submited by: ");
            sb.AppendLine(this.Customer.FirstName.ToString());
            sb.Append("Description: ");
            sb.AppendLine(this.Description);
            sb.Append("Rated at: ");
            sb.Append(this.Rating);
            sb.AppendLine("Starts");

            return sb.ToString();
        }
    }
}