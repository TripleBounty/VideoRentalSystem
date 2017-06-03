using VideoRentalSystem.Data.Repository.Contracts;

namespace VideoRentalSystem.Data.Contracts
{
    public interface IDatabase
    {
        ICountryRepository Countries { get; }

        IEmployeesRepository Employees { get; }

        IReviewRepository Reviews { get; }

        IFilmRepository Film { get; }

        int Complete();
    }
}
