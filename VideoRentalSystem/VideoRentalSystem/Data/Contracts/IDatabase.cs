namespace VideoRentalSystem.Data.Contracts
{
    public interface IDatabase
    {
        ICountryRepository Countries { get; }

        IEmployeesRepository Employees { get; }

        IFilmRepository Film { get; }

        int Complete();
    }
}
