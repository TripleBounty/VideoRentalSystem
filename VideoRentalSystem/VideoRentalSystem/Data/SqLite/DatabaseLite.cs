using VideoRentalSystem.Data.Repository;
using VideoRentalSystem.Data.SqLite.Contracts;

namespace VideoRentalSystem.Data.SqLite
{
    public class DatabaseLite : IDatabaseLite
    {
        private readonly VideoRentalFilmAwardsContext context;

        public DatabaseLite(VideoRentalFilmAwardsContext context)
        {
            this.context = context;
            this.Organisations = new OrganisationRepository(context);
            this.Awards = new AwardRepository(context);
        }

        public IOrganisationRepository Organisations { get; private set; }

        public IAwardRepository Awards { get; private set; }

        public int Complete()
        {
            return this.context.SaveChanges();
        }
    }
}
