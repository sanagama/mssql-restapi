using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MSSqlWebapi.Models
{
    public class Table
    {        
        [Key]
        public string Name { get; set; }
        public virtual ICollection<Column> Columns { get; set; }
    }
}