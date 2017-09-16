using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MSSqlWebapi.Models
{
    public class Table
    {
        [Key]
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public virtual ICollection<Column> Columns { get; set; }
    }
}