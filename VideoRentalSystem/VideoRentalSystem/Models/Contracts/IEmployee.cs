using System.Collections.Generic;

namespace VideoRentalSystem.Models.Contracts
{
    public interface IEmployee
    {
        int Id { get; set; }

        string FirstName { get; set; }

        string LastName { get; set; }

        int Salary { get; set; }

        Employee Manager { get; set; }
    }
}