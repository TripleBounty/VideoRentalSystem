using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Models.Factories;

namespace VideoRentalSystem.Commands.CreateCommands
{
    public class LoadCustomerFromJSONCommand : ICommand
    {
        private readonly IDatabase db;
        private readonly IModelsFactory factory;

        public LoadCustomerFromJSONCommand(IDatabase db, IModelsFactory factory)
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
                foreach (var employee in data.customer)
                {
                    foreach (var currentEmployee in employee)
                    {
                        string firstName = currentEmployee.FirstName;
                        string lastName = currentEmployee.LastName;
                        DateTime birthDate = DateTime.ParseExact(currentEmployee.BirthDate.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);

                        var customer = this.factory.CreateCustomer(firstName, lastName, birthDate);

                        this.db.Customers.Add(customer);
                        this.db.Complete();
                    }
                }
            }

            return "Customers created";
        }
    }
}