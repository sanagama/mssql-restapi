using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SMO = Microsoft.SqlServer.Management.Smo;  
using SMOCommon = Microsoft.SqlServer.Management.Common;  

namespace MSSqlWebapi.Models
{
    public sealed class DatabaseResource : Resource
    {
        [Key]
        public string Name { get; set; }
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public Uri TSqlScript { get; set; }
        public Uri Tables { get; set; }
        private SMO.Database _smoDatabase;

        public DatabaseResource(SMO.Database smoDatabase)
        {
            this._smoDatabase = smoDatabase;
            
            this.Name = this._smoDatabase.Name;
            this.Id = this._smoDatabase.ID;
            this.CreateDate = this._smoDatabase.CreateDate;
        }        
    }
}