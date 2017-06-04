using System;
using VideoRentalSystem.Common.Validations;
using VideoRentalSystem.Models.Contracts;

namespace VideoRentalSystem.Models
{
    public class Employee : IEmployee
    {
        private string firstName;
        private string lastName;
        private int salary;
        private Employee manager;

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

        public string FirstName
        {
            get
            {
                return this.firstName;
            }

            set
            {
                EmployeeValidator.ValidateName(value);
                this.firstName = value;
            }
        }

        public string LastName
        {
            get
            {
                return this.lastName;
            }

            set
            {
                EmployeeValidator.ValidateLastName(value);
                this.lastName = value;
            }
        }

        public int Salary
        {
            get
            {
                return this.salary;
            }

            set
            {
                EmployeeValidator.ValidateSalary(value);
                this.salary = value;
            }
        }

        public virtual Employee Manager { get; set; }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}, {3}BGN", this.Id, this.firstName, this.lastName, this.salary);
        }
    }
}