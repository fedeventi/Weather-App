using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather_App
{
    internal class WorldCities
    {
        public int Id { get; set; }
        public string City { get; set; }

        public string Lat { get; set; }
        public string Long { get; set; }
        public string Country { get; set; }
    }
}
