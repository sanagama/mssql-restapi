using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MSSqlWebapi.Models
{
    public sealed class ColumnEntity
    {
        [Key]
        public string Name { get; set; }
        public DateTime Created { get; set; }
    }
}