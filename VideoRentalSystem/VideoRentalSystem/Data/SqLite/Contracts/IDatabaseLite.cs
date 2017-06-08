using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoRentalSystem.Data.SqLite.Contracts
{
    public interface IDatabaseLite
    {
        IOrganisationRepository Organisations { get; }

        IAwardRepository Awards { get; }

        int Complete();
    }
}
