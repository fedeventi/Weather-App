using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather_App.Model;

namespace Weather_App
{
    internal class BaseContext : DbContext
    {
        public DbSet<WorldCities> WorldCities { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("Server=localhost;port=3306;Database=world;User Id=root;Password=fedeventi25");
        }

    }
}
