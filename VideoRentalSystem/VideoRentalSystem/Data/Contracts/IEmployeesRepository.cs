using VideoRentalSystem.Models;

namespace VideoRentalSystem.Data.Contracts
{
    public interface IEmployeesRepository
    {
         void CreateEmployees(Employee employee);
    }
}
