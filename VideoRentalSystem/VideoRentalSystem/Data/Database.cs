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
            this.Employees = new EmployeesRepository(context);
            this.Reviews = new ReviewRepository(context);

        }

        public ICountryRepository Countries { get; private set; }

        public IEmployeesRepository Employees { get; private set; }
        public IReviewRepository Reviews { get; private set; }

        public int Complete()
        {
            return this.context.SaveChanges();
        }
    }
}