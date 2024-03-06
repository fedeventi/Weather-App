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

                    var result = context.cities.Where(x => x.City.Contains("buenos ")).Select(x=> $"{x.City},{x.Country.Substring(0,2)} " ).ToList();
                    foreach (var city in result) 
                        Console.WriteLine(city);


                    
                }
            }
            catch (Exception ex) { Console.WriteLine(ex); }
        }

      
    }
}
