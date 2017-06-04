using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoRentalSystem.Models
{
    public class Store
    {
        public Store(string name, Address address)
        {
            this.Name = name;
            this.Address = address;
            this.Employees = new HashSet<Employee>();
        }

        public Store()
        {
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual Address Address { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"{this.Id} {this.Name}");
            stringBuilder.AppendLine(Address.ToString());
            stringBuilder.AppendLine(string.Join("\n", this.Employees));

            return stringBuilder.ToString();
        }
    }
}
