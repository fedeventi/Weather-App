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
        BaseContext _db= new BaseContext();
        
        public void Get()
        {
            //try
            //{

            //    using (var context = new BaseContext())
            //    {

                    
                   Console.WriteLine(_db.Database.GetDbConnection().State);

                    // Comprobar state
            //    }
            //}
            //catch(Exception ex) { Console.WriteLine("no me pude conectar"); }
        }
        
        public void Get_Returns_Cities()
        {
            

            using var context = new BaseContext();
            context.WorldCities.Add(new WorldCities { City = "Buenos Aires" });
            Console.WriteLine(context.WorldCities.ToList().Where(x=>x.Country=="Argentina"));

        }
    }
}
