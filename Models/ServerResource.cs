using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SMO = Microsoft.SqlServer.Management.Smo;  
using SMOCommon = Microsoft.SqlServer.Management.Common;  

namespace MSSqlWebapi.Models
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
            base.links[Constants.LinkNameSelf] = new Uri(
                urlHelper.RouteUrl(
                Constants.ApiRouteNameServer,   // Route
                null,                           // route parameters
                urlHelper.ActionContext.HttpContext.Request.Scheme   // scheme
            ));

            this.Databases = new Uri(
                urlHelper.RouteUrl(
                Constants.ApiRouteNameDatabases,    // Route
                null,                               // route parameters
                urlHelper.ActionContext.HttpContext.Request.Scheme   // scheme
            ));
        }
    }
}