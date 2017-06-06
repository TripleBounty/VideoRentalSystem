using System.Collections.Generic;
using System.Text;

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
            this.Employees = new HashSet<Employee>();
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

            if (this.Employees.Count != 0)
            {
                stringBuilder.AppendLine("Employees");
                stringBuilder.Append(string.Join("\n", this.Employees));
            }
            else
            {
                stringBuilder.Append("No employee");
            }

            return stringBuilder.ToString();
        }
    }
}
