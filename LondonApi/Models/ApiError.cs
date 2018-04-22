using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.ComponentModel;

namespace LondonApi.Models
{
    public class ApiError
    {
        public string Message { get; set; }
        public string Detail { get; set; }
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore, DefaultValueHandling =DefaultValueHandling.Ignore)]
        [DefaultValue("")]
        public string StackTrace { get; set; }
    }
}
