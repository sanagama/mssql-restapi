using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using SMO = Microsoft.SqlServer.Management.Smo;  
using SMOCommon = Microsoft.SqlServer.Management.Common;  

namespace MSSqlWebapi.Models
{
    public class RootResource : Resource
    {
        public string Name { get; set; }
        public string Product { get; set; }
        public string HostPlatform { get; set; }
        public string Edition { get; set; }
        public string VersionString { get; set; }
        public string NetName { get; set; }
        
        public Uri Databases { get; set; }
        private SMO.Server _smoServer;

        public RootResource(SMO.Server smoServer)
        {
            this._smoServer = smoServer;

            this.Name = this._smoServer.Name;
            this.Product = this._smoServer.Product;
            this.HostPlatform = this._smoServer.HostPlatform;
            this.Edition = this._smoServer.Edition;
            this.VersionString = this._smoServer.VersionString;
            this.NetName = this._smoServer.Information.NetName;
        }
    }
}