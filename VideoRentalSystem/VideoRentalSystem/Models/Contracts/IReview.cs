namespace VideoRentalSystem.Models.Contracts
{
    public interface IReview
    {
        int Id { get; set; }

       Film Film { get; set; }
        double Rating { get; set; }

        string Description { get; set; }
    }
}