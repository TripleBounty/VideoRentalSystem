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
            this.Films = new HashSet<Film>();
            this.Employees = new HashSet<Employee>();
        }

        private Store()
        {
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual Address Address { get; set; }

        public virtual ICollection<Film> Films { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"{this.Id} {this.Name}");
            stringBuilder.AppendLine(Address.ToString());
            stringBuilder.AppendLine(String.Join("\n", this.Employees));
            stringBuilder.AppendLine(String.Join("\n", this.Films));

            return stringBuilder.ToString();
        }
    }
}
