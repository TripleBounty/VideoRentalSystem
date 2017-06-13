using System.Data.Entity;
using VideoRentalSystem.Data.SqLite.Contracts;
using VideoRentalSystem.Models;

namespace VideoRentalSystem.Data.Repository
{
    public class OrganisationRepository : Repository<Organisation>, IOrganisationRepository
    {
        public OrganisationRepository(DbContext context) 
            : base(context)
        {
        }
    }
}
