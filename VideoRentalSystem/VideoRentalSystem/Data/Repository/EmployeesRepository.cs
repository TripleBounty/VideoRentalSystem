using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Models;

namespace VideoRentalSystem.Data.Repository
{
    public class EmployeesRepository : Repository<Employees>, IEmployeesRepository
    {

        public EmployeesRepository(VideoRentalContext context)
            : base(context)
        {
        }

        public void CreateEmployees(Employees employee)
        {
            VideoRentalContext.EmployeesTable.Add(employee);
        }

        private VideoRentalContext VideoRentalContext
        {
            get { return Context as VideoRentalContext; }
        }
    }
}

