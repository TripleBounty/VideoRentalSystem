namespace VideoRentalSystem.Data.Contracts
{
    public interface IDatabase
    {
        IUserRepository Users { get; }
        ICountryRepository Countries { get; }
        IEmployeesRepository Employees { get; }

        int Complete();
    }
}
