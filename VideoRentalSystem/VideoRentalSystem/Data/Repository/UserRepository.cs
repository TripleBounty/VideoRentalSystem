using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Models;

namespace VideoRentalSystem.Data.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(VideoRentalContext context)
            : base(context)
        {
        }

        public IEnumerable<IUser> GetUsersOrderById(int count)
        {
            return VideoRentalContext.UsersTable.OrderByDescending(u => u.ID).ToList();
        }

        private VideoRentalContext VideoRentalContext
        {
            get { return Context as VideoRentalContext; }
        }
    }
}
