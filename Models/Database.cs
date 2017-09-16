using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MSSqlWebapi.Models
{
    public class Database
    {
        [Key]
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public virtual ICollection<Table> Tables { get; set; }
    }
}