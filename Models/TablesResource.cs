using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SMO = Microsoft.SqlServer.Management.Smo;  
using SMOCommon = Microsoft.SqlServer.Management.Common;  

namespace MSSqlWebapi.Models
{
    public sealed class TablesResource : Resource
    {
        private ServerContext _context;
        private List<DatabaseResource> _databases;
        private SMO.Server SmoServer { get { return this._context.SmoServer; } }
        public int DatabaseCount { get { return this._databases.Count; } }
        public List<DatabaseResource> Databases { get { return this._databases; } }

        public TablesResource(ServerContext context, IUrlHelper urlHelper)
        {
            this._context = context;
            this.UpdateLinks(urlHelper);

            // Get databases
            this.SmoServer.Databases.Refresh();
            this._databases = new List<DatabaseResource>();
            foreach(SMO.Database smoDb in this.SmoServer.Databases)
            {
                var dbResource = new DatabaseResource(this._context, smoDb.Name, urlHelper);
                this._databases.Add(dbResource);
            }
        }

        public override void UpdateLinks(IUrlHelper urlHelper)
        {
            // self
            base.links[Constants.LinkNameSelf] = new Uri(
                urlHelper.RouteUrl(
                RouteNames.Databases,
                null,
                urlHelper.ActionContext.HttpContext.Request.Scheme
            ));

            // parent
            base.links[Constants.LinkNameParent] = new Uri(
                urlHelper.RouteUrl(
                RouteNames.Server,   // Route
                null,                // route parameters
                urlHelper.ActionContext.HttpContext.Request.Scheme   // scheme
            ));
        }
    }
}