﻿using Ninject;
using VideoRentalSystem.Builder;
using VideoRentalSystem.Core.Contracts;

namespace VideoRentalSystem
{
    public class Startup
    {
        public static void Main(string[] args)
        {
            // //intialize db
            // //using db for the first time
            // using (var db = new VideoRentalContext())
            // {
            //     Console.Write("Enter name for Country : ");
            //     var name = Console.ReadLine();
            //     Console.Write("Enter name for CountryCode : ");
            //     var code = Console.ReadLine();
            //
            //     var country = new Country { CountryName = name, CountryCode = code };
            //     db.Countries.Add(country);
            //     db.SaveChanges();
            //
            //     var query = from b in db.Countries
            //                 orderby b.CountryName
            //                 select b;
            //
            //     foreach (var item in query)
            //     {
            //         Console.WriteLine(item.CountryName);
            //     }
            // }

            IKernel kernel = new StandardKernel(new BuildManager());

            IEngine engine = kernel.Get<IEngine>();
            engine.Start();
        }
    }
}