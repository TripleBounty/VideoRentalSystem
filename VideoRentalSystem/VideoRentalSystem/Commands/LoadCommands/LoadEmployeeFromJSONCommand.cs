using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Models.Factories;

namespace VideoRentalSystem.Commands.CreateCommands
{
    public class LoadEmployeeFromJSONCommand : ICommand
    {
        private readonly IDatabase db;
        private readonly IModelsFactory factory;

        public LoadEmployeeFromJSONCommand(IDatabase db, IModelsFactory factory)
        {
            this.db = db;
            this.factory = factory;
        }

        public string Execute(IList<string> parameters)
        {
            string fileLoc = parameters[0];
            using (StreamReader r = new StreamReader(fileLoc))
            {
                string json = r.ReadToEnd();
                dynamic data = JObject.Parse(json);
                foreach (var employee in data.employee)
                {
                    foreach (var currentEmployee in employee)
                    {
                        string firstName = currentEmployee.FirstName;
                        string lastName = currentEmployee.LastName;
                        int salary;
                        int managerId;

                        try
                        {
                            salary = currentEmployee.Salary;
                            managerId = currentEmployee.ManagerId;
                        }
                        catch (Exception)
                        {
                            throw new ArgumentException(string.Format(
                                "Input parameters are not in the correct format!" +
                                Environment.NewLine +
                                "The correct format is: FirstName string;LastName string;Salary int; ManagerId int"));
                        }

                        var employeeObj = this.db.Employees.SingleOrDefault(e => e.Id == managerId);
                        if (employeeObj == null)
                        {
                            return "Manager with such id doesn't exist! /n Hint: to create a Manager use CreateManager command";
                        }

                        var employeeToCreate = this.factory.CreateEmployee(firstName, lastName, salary, employeeObj);

                        this.db.Employees.Add(employeeToCreate);
                        this.db.Complete();
                    }
                }
            }

            return "Employees created";
        }
    }
}