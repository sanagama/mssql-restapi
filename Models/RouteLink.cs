using System;
using Newtonsoft.Json;

//
// Inspired by: https://github.com/nbarbettini/BeautifulRestApi
//
namespace MSSqlWebapi.Models
{
    public sealed class RouteLink : Link
    {
        [JsonIgnore]
        public string RouteName { get; set; }

        [JsonIgnore]
        public object RouteValues { get; set; }
    }
}