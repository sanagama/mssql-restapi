using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MSSqlWebapi.Models
{
    public sealed class TableEntity
    {        
        [Key]
        public string Name { get; set; }
        public ICollection<ColumnEntity> Columns { get; set; }
    }
}