using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LondonApi.Models
{
    public abstract class Resources
    {
        [JsonProperty(Order =-2)]
        public string Href { get; set; }
    }
}
