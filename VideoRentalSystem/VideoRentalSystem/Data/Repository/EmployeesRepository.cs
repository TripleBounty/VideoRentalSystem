using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Models;

namespace VideoRentalSystem.Data.Repository
{
    public class EmployeesRepository : Repository<Employee>, IEmployeesRepository
    {

        public EmployeesRepository(VideoRentalContext context)
            : base(context)
        {
        }

        public void CreateEmployees(Employee employee)
        {
            VideoRentalContext.EmployeesTable.Add(employee);
        }

        private VideoRentalContext VideoRentalContext
        {
            get { return Context as VideoRentalContext; }
        }
    }
}

