using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VideoRentalSystem.Models.Contracts;

namespace VideoRentalSystem.Models
{
    public class Employee : IEmployee
    {
        public Employee(string firstName, string lastName, int salary, Employee manager)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Salary = salary;
            this.Manager = manager;
        }

        private Employee()
        {
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Salary { get; set; }

        public Employee Manager { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3} {4}", this.Id, this.FirstName, this.LastName, this.Salary, this.Manager);
        }
    }
}