using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SMO = Microsoft.SqlServer.Management.Smo;  
using SMOCommon = Microsoft.SqlServer.Management.Common;  

namespace MSSqlRestApi.Models
{
    public class ServerResource : Resource
    {
        public string Name { get { return this.SmoServer.Name; }  }
        public string Product { get { return this.SmoServer.Product; } }
        public string HostPlatform { get{ return this.SmoServer.HostPlatform; } }
        public string Edition { get { return this.SmoServer.Edition; } }
        public string VersionString { get { return this.SmoServer.VersionString; } }
        
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