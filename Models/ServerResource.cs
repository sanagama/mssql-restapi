using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Events;
using SMO = Microsoft.SqlServer.Management.Smo;  
using SMOCommon = Microsoft.SqlServer.Management.Common;  

namespace MSSqlRestApi.Models
{
    public class ServerResource : Resource
    {
        public string ServerName { get { return this.SmoServer.Name; }  }
        
        public string Product
        {
            // Azure SQL DB and Azure SQL DW don't support the SMO Product' property
            get
            {
                string retVal = "Not Available";
                if(this.SmoServer.ServerType == SMOCommon.DatabaseEngineType.Standalone)
                {
                    retVal = this.SmoServer.Product;
                }
                return retVal;
            }
        }
        public string HostPlatform { get{ return this.SmoServer.HostPlatform; } }
        public string ServerType { get { return this.SmoServer.ServerType.ToString(); } }
        public string Edition { get { return this.SmoServer.Edition; } }
        public string EngineEdition { get { return this.SmoServer.EngineEdition.ToString(); } }
        public string ProductLevel { get { return this.SmoServer.ProductLevel; } }
        public string Collation { get { return this.SmoServer.Collation; }  }
        public string VersionString { get { return this.SmoServer.VersionString; } }
        public string ResourceVersionString { get { return this.SmoServer.ResourceVersionString; } }


        public Uri Databases { get; set; }
        private ServerContext _context;
        private SMO.Server SmoServer { get { return this._context.SmoServer; } }

        public ServerResource(ServerContext context, IUrlHelper urlHelper)
        {
            this._context = context;
            this._context.SmoServer.Refresh();
            this.UpdateLinks(urlHelper);
        }

        public override void UpdateLinks(IUrlHelper urlHelper)
        {
            // self
            base.links[Constants.LinkNameSelf] = new Uri(
                urlHelper.RouteUrl(
                RouteNames.Server,  // Route
                null,               // route parameters
                urlHelper.ActionContext.HttpContext.Request.Scheme   // scheme
            ));

            // databases
            this.Databases = new Uri(
                urlHelper.RouteUrl(
                RouteNames.Databases,   // Route
                null,                   // route parameters
                urlHelper.ActionContext.HttpContext.Request.Scheme   // scheme
            ));
        }
    }
}