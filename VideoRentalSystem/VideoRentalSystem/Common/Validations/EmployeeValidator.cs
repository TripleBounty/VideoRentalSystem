using System;

namespace VideoRentalSystem.Common.Validations
{
    public static class EmployeeValidator
    {
        private const int MinSalary = 0;
        private const int MaxSalary = 150000;
        private const int MinNameLenght = 1;
        private const int MaxNameLenght = 15;        

        public static void ValidateName(string value)
        {
            if (value.Length < MinNameLenght || value.Length > MaxNameLenght)
            {
                throw new ArgumentOutOfRangeException(
                      $"First Name - must be between 1 and 15.");
            }
        }

        public static void ValidateLastName(string value)
        {
            if (value.Length < MinNameLenght || value.Length > MaxNameLenght)
            {
                throw new ArgumentOutOfRangeException(
                      $"Last Name - must be between 1 and 15.");
            }
        }

        public static void ValidateSalary(int value)
        {
            if (value < MinSalary || value > MaxSalary)
            {
                throw new ArgumentOutOfRangeException(
                      $"The Salary must be between 0 and 150000.");
            }
        }
    }
}