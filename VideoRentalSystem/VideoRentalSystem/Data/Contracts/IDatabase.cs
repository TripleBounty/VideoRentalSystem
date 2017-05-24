using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoRentalSystem.Data.Contracts;

namespace VideoRentalSystem.Data
{
    public interface IDatabase
    {
        IUserRepository Users { get; }
        ICountryRepository Countries { get; }

        int Complete();

        void Dispose();
    }
}
