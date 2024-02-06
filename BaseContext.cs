using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather_App;

namespace Weather_App
{
    internal class BaseContext : DbContext
    {
        public BaseContext(DbContextOptions<BaseContext> options): base (options) { }
        public DbSet<WorldCities> cities { get; set; }
        
       

    }
}
