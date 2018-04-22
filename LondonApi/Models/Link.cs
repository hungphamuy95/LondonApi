using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using Newtonsoft.Json;

namespace LondonApi.Models
{
    public class Link
    {
        public const string GetMethod = "GET";
        public static Link To(string routeName, object routeValues = null)
            => new Link
            {
                RouteName = routeName,
                RouteValues = routeValues,
                Method = GetMethod,
                Relations = null
            };
        [JsonProperty(Order =-4)]
        public string Href { get; set; }
        [JsonProperty(Order =-3, NullValueHandling =NullValueHandling.Ignore, DefaultValueHandling =DefaultValueHandling.Ignore)]
        [DefaultValue(GetMethod)]
        public string Method { get; set; }
        [JsonProperty(Order =-2, PropertyName ="rel", NullValueHandling =NullValueHandling.Ignore)]
        public string [] Relations { get; set; }

        // Stores the route name before being overwritten
        [JsonIgnore]
        public string RouteName { get; set; }
        [JsonIgnore]
        public object RouteValues { get; set; }
    }
}
