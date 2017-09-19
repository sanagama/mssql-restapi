using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SMO = Microsoft.SqlServer.Management.Smo;  
using SMOCommon = Microsoft.SqlServer.Management.Common;  

namespace MSSqlWebapi.Models
{
    public class Server
    {
        [Key]
        public string Name { get; set; }
        public string Product { get; set; }
        public string HostPlatform { get; set; }
        public string Edition { get; set; }
        public string VersionString { get; set; }
        public string Information {get; set;}

        public ICollection<Link> Links { get; set; }
        public virtual ICollection<Database> Databases { get; set; }

        private SMO.Server _smoServer;

        public Server(SMO.Server smoServer)
        {
            _smoServer = smoServer;
            Initialize();
        }

        private void Initialize()
        {
            this.Name = _smoServer.Name;
            this.Product = _smoServer.Product;
            this.HostPlatform = _smoServer.HostPlatform;
            this.Edition = _smoServer.Edition;
            this.VersionString = _smoServer.VersionString;
            this.Information = _smoServer.Information.ToString();

            this.Databases = new List<Database>();
        }
    }
}