using System;
using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Data.Repository;
using VideoRentalSystem.Data.Repository.Contracts;

namespace VideoRentalSystem.Data
{
    public class Database : IDatabase
    {
        private readonly VideoRentalContext context;

        public Database(VideoRentalContext context)
        {
            this.context = context;
            this.Countries = new CountryRepository(context);
            this.Towns = new TownRepository(context);
            this.Addesses = new AddessRepository(context);
            this.Employees = new EmployeesRepository(context);
            this.Customers = new CustomerRepository(context);
            this.Reviews = new ReviewRepository(context);
            this.Films = new FilmRepository(context);
            this.FilmGenres = new FilmGenreRepository(context);
            this.FilmRating = new FilmRatingRepository(context);
            this.Stores = new StoreRepository(context);
            this.Storages = new StorageRepository(context);
            this.FilmStaffs = new FilmStaffRepository(context);
            this.Award = new AwardRepository(context);
        }

        public ICountryRepository Countries { get; private set; }

        public ITownRepository Towns { get; private set; }

        public IAddessRepository Addesses { get; private set; }

        public IEmployeesRepository Employees { get; private set; }

        public ICustomerRepository Customers { get; private set; }

        public IReviewRepository Reviews { get; private set; }

        public IFilmRepository Films { get; private set; }

        public IFilmGenreRepository FilmGenres { get; private set; }

        public IFilmRatingRepository FilmRating { get; private set; }

        public IStoreRepository Stores { get; private set; }

        public IStorageRepository Storages { get; private set; }

        public IFilmStaffRepository FilmStaffs { get; private set; }

        public IAwardRepository Award { get; private set; }

        public int Complete()
        {
            return this.context.SaveChanges();
        }
    }
}