using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MSSqlWebapi.Models
{
    public class Link
    {
        public string rel;
        public Uri href;
    }
}