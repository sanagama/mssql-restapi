using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MSSqlWebapi.Models
{
    public sealed class DatabaseEntity
    {
        [Key]
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public ICollection<TableEntity> Tables { get; set; }
    }
}