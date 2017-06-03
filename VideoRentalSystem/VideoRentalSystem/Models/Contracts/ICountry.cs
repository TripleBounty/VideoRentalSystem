namespace VideoRentalSystem.Models.Contracts
{
    public interface ICountry
    {
        int Id { get; set; }

        string Name { get; set; }

        string Code { get; set; }
    }
}
