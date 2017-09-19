using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

//
// Inspired by: https://github.com/nbarbettini/BeautifulRestApi
//
namespace MSSqlWebapi.Models
{
    public class Collection<T> : Resource
    {
        public const string CollectionRelation = "collection";

        public T[] Value { get; set; }
    }
}