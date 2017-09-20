using System;
using System.Collections.Generic;
using Newtonsoft.Json;

//
// Inspired by: https://github.com/nbarbettini/BeautifulRestApi
//
namespace MSSqlWebapi.Models
{
    public abstract class Resource
    {
        [JsonProperty(Order = -4)]
        public Uri self { get; set; }

        [JsonProperty(Order = -4)]
        public Uri parent { get; set; }
    }
}