using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using VideoRentalSystem.Data;
using VideoRentalSystem.Models;

namespace VideoRentalSystem
{
    public class Startup
    {
        public static void Main(string[] args)
        {
            //intialize db
            //using db for the first time
            using (var db = new VideoRentalContext())
            {
                Console.Write("Enter name for Country : ");
                var name = Console.ReadLine();
                Console.Write("Enter name for CountryCode : ");
                var code = Console.ReadLine();

                var country = new Country { CountryName = name, CountryCode = code };
                db.Countries.Add(country);
                db.SaveChanges();

                var query = from b in db.Countries
                            orderby b.CountryName
                            select b;

                foreach (var item in query)
                {
                    Console.WriteLine(item.CountryName);
                }
            }
        }
    }
}
