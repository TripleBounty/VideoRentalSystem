namespace VideoRentalSystem.Models.Contracts
{
    public interface IReview
    {
        int Id { get; set; }

        int FilmId { get; set; }

        double Rating { get; set; }

        string Description { get; set; }
    }
}