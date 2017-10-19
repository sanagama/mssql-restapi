using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SMO = Microsoft.SqlServer.Management.Smo;  
using SMOCommon = Microsoft.SqlServer.Management.Common;  
using Serilog;
using Serilog.Events;

namespace MSSqlWebapi.Models
{
    public sealed class TablesInSchemaResource : Resource
    {        
        public string SchemaName { get; set; }
        public string ParentDatabase { get; set; }
        public Dictionary<string, Uri> Tables { get; set; }
        private ServerContext _context;
        
        public TablesInSchemaResource(ServerContext context, string dbName, string schemaName, IUrlHelper urlHelper)
        {
            this._context = context;
            this.ParentDatabase = dbName;
            this.SchemaName = schemaName;
            this.Tables = new Dictionary<string, Uri>();

            // Get database by name
            this._context.SmoServer.Databases.Refresh();
            SMO.Database smoDb = this._context.SmoServer.Databases[dbName];
            if (smoDb == null)
            {
                throw new SMO.SmoException(String.Format("Database '{0}' not found.", dbName));
            }

            // Get tables in the specified schema
            smoDb.Tables.Refresh();
            var query = from table in smoDb.Tables.Cast<SMO.Table>()
            where !table.IsSystemObject && table.Schema.Equals(this.SchemaName, StringComparison.OrdinalIgnoreCase)
            select table;

            foreach(var table in query)
            {
                var tableName = table.Name;
                this.Tables[tableName] = new Uri(
                    urlHelper.RouteUrl(
                    RouteNames.Table,
                    new
                    {
                        dbName = this.ParentDatabase,
                        schemaName = this.SchemaName,
                        tableName = tableName
                    },
                    urlHelper.ActionContext.HttpContext.Request.Scheme
                ));
            }

            this.UpdateLinks(urlHelper);
        }
        
        public override void UpdateLinks(IUrlHelper urlHelper)
        {
            // self
            base.links[Constants.LinkNameSelf] = new Uri(
                urlHelper.RouteUrl(
                RouteNames.TablesInSchema,
                new
                {
                    dbName = this.ParentDatabase,
                    schemaName = this.SchemaName
                },
                urlHelper.ActionContext.HttpContext.Request.Scheme
            ));

            // parent
            base.links[Constants.LinkNameParent] = new Uri(
                urlHelper.RouteUrl(
                RouteNames.Database,
                new { dbName = this.ParentDatabase },
                urlHelper.ActionContext.HttpContext.Request.Scheme
            ));
        }
    }
}