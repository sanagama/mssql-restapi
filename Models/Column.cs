using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MSSqlWebapi.Models
{
    public class Column
    {
        [Key]
        public string Name { get; set; }
        public DateTime Created { get; set; }
    }
}