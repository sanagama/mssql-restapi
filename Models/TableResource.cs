using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SMO = Microsoft.SqlServer.Management.Smo;  
using SMOCommon = Microsoft.SqlServer.Management.Common;  

namespace MSSqlWebapi.Models
{
    public sealed class TableResource : Resource
    {        
        public string Name { get; set; }
        public ICollection<ColumnEntity> Columns { get; set; }
    }
}