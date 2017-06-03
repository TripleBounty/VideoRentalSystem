using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Data.Repository.Contracts;
using VideoRentalSystem.Models;
using VideoRentalSystem.Models.Contracts;

namespace VideoRentalSystem.Data.Repository
{
    //TODO: may or maynot be coupled
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {

        public ReviewRepository(VideoRentalContext context)
            : base(context)
        {
        }

        private VideoRentalContext VideoRentalContext
        {
            get { return Context as VideoRentalContext; }
        }
    }
}

