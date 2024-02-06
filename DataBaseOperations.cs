using Microsoft.EntityFrameworkCore;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather_App
{
    public class DataBaseOperations
    {
        
        public void Get()
        {
            var options = new DbContextOptionsBuilder<BaseContext>().UseMySQL("server=localhost;database=world;user=root;password=fedeventi25").Options;

            try
            {

                using (var context = new BaseContext(options))
                {

                    var result = context.cities.Where(x => x.Country == "Argentina").Select(x => x.City).ToList();
                    foreach (var city in result) 
                        Console.WriteLine(city);


                    
                }
            }
            catch (Exception ex) { Console.WriteLine(ex); }
        }

        public void Get_Returns_Cities()
        {

            var options = new DbContextOptionsBuilder<BaseContext>().UseMySQL("server=localhost;database=world;user=root;password=fedeventi25").Options;
            using var context = new BaseContext(options);
            context.cities.Add(new WorldCities { City = "Buenos Aires" });
            Console.WriteLine(context.cities.ToList().Where(x=>x.Country=="Argentina"));

        }
    }
}
