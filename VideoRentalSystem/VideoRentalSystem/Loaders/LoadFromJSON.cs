using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoRentalSystem.Models;

namespace VideoRentalSystem.Loaders
{
    public class LoadFromJSON
    {
        /// <summary>
        /// THIS IS A "HOW TO" work with both JSON string and JSON OBJECT
        /// </summary>
        public static void LoadJson()
        {
            using (StreamReader r = new StreamReader("Reviews.json"))
            {
                string json = r.ReadToEnd();
                dynamic array = JsonConvert.DeserializeObject(json);

                dynamic data = JObject.Parse(json);
                Console.WriteLine(data.FilmId);
                Console.WriteLine(data.Rating);
                Console.WriteLine(data.Description);

                foreach (var item in array)
                {
                    Console.WriteLine("{0}", item);
                }
            }            
        }
    }
}
