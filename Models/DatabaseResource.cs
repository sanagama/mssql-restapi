using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.Common;
using Microsoft.AspNetCore.Mvc;
using SMO = Microsoft.SqlServer.Management.Smo;  
using SMOCommon = Microsoft.SqlServer.Management.Common;  

namespace MSSqlWebapi.Models
{
    public sealed class DatabaseResource : Resource
    {
        public string Name { get { return this.SmoDatabase.Name; } }
        public int Id { get { return this.SmoDatabase.ID; } }
        public DateTime CreateDate { get { return this.SmoDatabase.CreateDate; } }
        public int TableCount { get { return this.SmoDatabase.Tables.Count; } }
        public int ViewCount { get { return this.SmoDatabase.Views.Count; } }
        public Uri Script { get; set; }
        public Uri Tables { get; set; }
        private ServerContext _context;
        private SMO.Database _smoDatabase;
        private SMO.Server SmoServer { get { return this._context.SmoServer; } }        
        private SMO.Database SmoDatabase { get { return this._smoDatabase; } }        
        public DatabaseResource(ServerContext context, string dbName, IUrlHelper urlHelper)
        {
            this._context = context;

            // Get database by name
            this.SmoServer.Databases.Refresh();
            this._smoDatabase = this.SmoServer.Databases[dbName];
            if (this._smoDatabase == null)
            {
                throw new SMO.SmoException(String.Format("Database {0} not found", dbName));
            }
            
            this.UpdateLinks(urlHelper);
        }

        public override void UpdateLinks(IUrlHelper urlHelper)
        {
            // self
            base.links[Constants.LinkNameSelf] = new Uri(
                urlHelper.RouteUrl(
                RouteNames.Database,
                new { dbName = this.Name },
                urlHelper.ActionContext.HttpContext.Request.Scheme
            ));

            // parent
            base.links[Constants.LinkNameParent] = new Uri(
                urlHelper.RouteUrl(
                RouteNames.Root,    // Route
                null,               // route parameters
                urlHelper.ActionContext.HttpContext.Request.Scheme   // scheme
            ));

            // script
            this.Script = new Uri(
                urlHelper.RouteUrl(
                RouteNames.DatabaseScript,
                new { dbName = this.Name },
                urlHelper.ActionContext.HttpContext.Request.Scheme
            ));

            // tables
            this.Tables = new Uri(
                urlHelper.RouteUrl(
                RouteNames.Tables,
                new { dbName = this.Name },
                urlHelper.ActionContext.HttpContext.Request.Scheme
            ));
        }
    }
}