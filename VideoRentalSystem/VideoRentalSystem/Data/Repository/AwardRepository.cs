using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Models;

namespace VideoRentalSystem.Data.Repository
{
    public class AwardRepository : Repository<Award>, IAwardRepository
    {
        public AwardRepository(VideoRentalContext context)
         : base(context)
        {
        }
    }
}
