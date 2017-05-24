using System.Collections.Generic;
using VideoRentalSystem.Models.Contracts;

namespace VideoRentalSystem.Data.Contracts
{
    public interface IUserRepository
    {
        IEnumerable<IUser> GetUsersOrderById(int count);
    }
}
