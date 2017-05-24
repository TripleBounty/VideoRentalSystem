using System.Collections.Generic;
using System.Linq;
using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Models;
using VideoRentalSystem.Models.Contracts;

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
            return VideoRentalContext.UsersTable.OrderByDescending(u => u.Id).ToList();
        }

        private VideoRentalContext VideoRentalContext
        {
            get { return Context as VideoRentalContext; }
        }
    }
}
