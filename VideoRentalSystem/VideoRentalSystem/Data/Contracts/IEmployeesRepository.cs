using VideoRentalSystem.Models;

namespace VideoRentalSystem.Data.Contracts
{
    public interface IEmployeesRepository
    {
            //TODO: inject interface 
            void CreateEmployees(Employees employee);
    }
}
