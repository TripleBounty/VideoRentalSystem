using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoRentalSystem.Data.Contracts;

namespace VideoRentalSystem.Data.Repository
{
    public class Database : IDatabase
    {
        private readonly VideoRentalContext context;

        public Database(VideoRentalContext context)
        {
            this.context = context;
            this.Users = new UserRepository(context);
            this.Countries = new CountryRepository(context);
        }

        public IUserRepository Users { get; private set; }
        public ICountryRepository Countries { get; private set; }

        public int Complete()
        {
            return context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
