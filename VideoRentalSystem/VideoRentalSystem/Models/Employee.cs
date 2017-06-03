﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VideoRentalSystem.Models.Contracts;

namespace VideoRentalSystem.Models
{
    public class Employee : IEmployee
    {
        private Employee()
        {
        }

        public Employee(string firstName, string lastName, int salary, Employee manager)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Salary = salary;
            this.Manager = manager;
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Salary { get; set; }

        public Employee Manager { get; set; }
    }
}