using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
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
                        int salary = currentEmployee.Salary;
                        int managerId = currentEmployee.ManagerId;

                        var employeeObj = this.db.Employees.SingleOrDefault(e => e.Id == managerId);

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