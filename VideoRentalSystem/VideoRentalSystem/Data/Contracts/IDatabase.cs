namespace VideoRentalSystem.Data.Contracts
{
    public interface IDatabase
    {
        IUserRepository Users { get; }
        ICountryRepository Countries { get; }

        int Complete();

        void Dispose();
    }
}
