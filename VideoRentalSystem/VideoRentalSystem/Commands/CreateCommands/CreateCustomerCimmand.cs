using System;
using System.Collections.Generic;
using System.Globalization;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Models.Factories;

namespace VideoRentalSystem.Commands.CreateCommands
{
    public class CreateCustomerCommand : ICommand
    {
        private readonly IDatabase db;
        private readonly IModelsFactory factory;

        public CreateCustomerCommand(IDatabase db, IModelsFactory factory)
        {
            this.db = db;
            this.factory = factory;
        }

        public string Execute(IList<string> parameters)
        {
            var firstName = parameters[0];
            var lastName = parameters[1];
            DateTime birthDate = DateTime.ParseExact(parameters[2], "dd/MM/yyyy", CultureInfo.InvariantCulture);
            
            var customer = this.factory.CreateCustomer(firstName, lastName, birthDate);

            this.db.Customers.Add(customer);
            this.db.Complete();

            return "Customer created";
        }
    }
}