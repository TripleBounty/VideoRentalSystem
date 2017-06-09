using VideoRentalSystem.Common.Validations;

namespace VideoRentalSystem.Models
{
    public class Employee
    {
        private string firstName;
        private string lastName;
        private int salary;

        public Employee(string firstName, string lastName, int salary, Employee manager)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Salary = salary;
            this.Manager = manager;
        }

        public Employee()
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