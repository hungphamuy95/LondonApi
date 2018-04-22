using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LondonApi.Models
{
    public class Room:Resources
    {
        public string Name { get; set; }
        public decimal Rate { get; set; }

    }
}
