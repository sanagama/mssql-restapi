using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SMO = Microsoft.SqlServer.Management.Smo;  
using SMOCommon = Microsoft.SqlServer.Management.Common;  

namespace MSSqlWebapi.Models
{
    public class ServerResource : Resource
    {
        public string Name { get; set; }
        public string Product { get; set; }
        public string HostPlatform { get; set; }
        public string Edition { get; set; }
        public string VersionString { get; set; }
        
        public Link Databases { get; set; }

        private SMO.Server _smoServer;

        public ServerResource(SMO.Server smoServer)
        {
            _smoServer = smoServer;

            this.Name = _smoServer.Name;
            this.Product = _smoServer.Product;
            this.HostPlatform = _smoServer.HostPlatform;
            this.Edition = _smoServer.Edition;
            this.VersionString = _smoServer.VersionString;
        }
    }
}