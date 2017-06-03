﻿using VideoRentalSystem.Data.Contracts;
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
            this.Film = new FilmRepository(context);
        }

        public ICountryRepository Countries { get; private set; }

        public ITownRepository Towns { get; private set; }

        public IAddessRepository Addesses { get; private set; }

        public IEmployeesRepository Employees { get; private set; }

        public ICustomerRepository Customers { get; private set; }

        public IReviewRepository Reviews { get; private set; }

        public IFilmRepository Film { get; private set; }

        public int Complete()
        {
            return this.context.SaveChanges();
        }
    }
}