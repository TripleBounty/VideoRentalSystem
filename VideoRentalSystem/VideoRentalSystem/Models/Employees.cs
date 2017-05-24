using System.ComponentModel.DataAnnotations;
using VideoRentalSystem.Models.Contracts;

namespace VideoRentalSystem.Models
{
    public class Employees : IEmployees
    {
        public Employees (string firstName, string lastName, int salary, int managerId)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Salary = salary;
            this.ManagerId = managerId;
        }

        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public int Salary { get; set; }

        public int ManagerId { get; set; }
    }
}