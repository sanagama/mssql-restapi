using System;
using Newtonsoft.Json;

//
// Inspired by: https://github.com/nbarbettini/BeautifulRestApi
//
namespace MSSqlWebapi.Models
{
    public abstract class Resource : Link
    {
        [JsonIgnore]
        public Link Self { get; set; }
    }
}