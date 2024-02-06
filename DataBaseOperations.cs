using Microsoft.EntityFrameworkCore;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather_App
{
    internal class DataBaseOperations
    {
        BaseContext _db= new BaseContext();
        
        public void Get()
        {
            _db.WorldCities.ToList();
        }
        
        public void Get_Returns_Cities()
        {
            

            using var context = new BaseContext();
            context.WorldCities.Add(new WorldCities { City = "Buenos Aires" });
            Console.WriteLine(context.WorldCities.ToList().Where(x=>x.Country=="Argentina"));

        }
    }
}
