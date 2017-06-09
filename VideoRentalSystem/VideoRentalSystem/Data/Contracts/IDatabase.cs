using VideoRentalSystem.Data.Repository.Contracts;

namespace VideoRentalSystem.Data.Contracts
{
    public interface IDatabase
    {
        ICountryRepository Countries { get; }

        ITownRepository Towns { get; }

        IAddessRepository Addesses { get; }

        IEmployeesRepository Employees { get; }

        ICustomerRepository Customers { get; }

        IReviewRepository Reviews { get; }

        IFilmRepository Films { get; }

        IFilmGenreRepository FilmGenres { get; }

        IFilmRatingRepository FilmRating { get; }

        IStoreRepository Stores { get; }

        IStorageRepository Storages { get; }

        IFilmStaffRepository FilmStaffs { get; }

        int Complete();
    }
}
