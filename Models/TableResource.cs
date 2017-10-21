using System;
//using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Events;
using SMO = Microsoft.SqlServer.Management.Smo;  
using SMOCommon = Microsoft.SqlServer.Management.Common;  

namespace MSSqlRestApi.Models
{
    public sealed class TableResource : Resource
    {        
        public string Name { get { return this._smoTable.Name; } }
        public int Id { get { return this._smoTable.ID; } }
        public string Database { get { return this._smoTable.Parent.Name; } }
        public string Schema { get { return this._smoTable.Schema; } }
        public DateTime CreateDate { get { return this._smoTable.CreateDate; } }
        public long RowCount { get { return this._smoTable.RowCount; } }
        public long ColumnCount { get { return this._smoTable.Columns.Count; } }
        public Uri Script { get; set; }
        public Uri Columns { get; set; }
        public Uri Top100Rows { get; set; }
        private ServerContext _context;
        private SMO.Table _smoTable;
        
        public TableResource(ServerContext context, string dbName, string schemaName, string tableName, IUrlHelper urlHelper)
        {
            this._context = context;

            // Get database by name
            this._context.SmoServer.Databases.Refresh();
            SMO.Database smoDb = this._context.SmoServer.Databases[dbName];
            if (smoDb == null)
            {
                throw new SMO.SmoException(String.Format("Database '{0}' not found.", dbName));
            }

            // Get table by name
            smoDb.Tables.Refresh();
            this._smoTable = smoDb.Tables[tableName, schemaName];
            if(this._smoTable == null)
            {
                throw new SMO.SmoException(String.Format("Table '{0}' not found in Schema '{1}' in Database '{1}'.",
                    tableName, schemaName, dbName));
            }
            
            this.UpdateLinks(urlHelper);
        }
        
        public override void UpdateLinks(IUrlHelper urlHelper)
        {
            // self
            base.links[Constants.LinkNameSelf] = new Uri(
                urlHelper.RouteUrl(
                RouteNames.Table,
                new
                {
                    dbName = this.Database,
                    schemaName = this.Schema,
                    tableName = this.Name
                },
                urlHelper.ActionContext.HttpContext.Request.Scheme   // scheme
            ));

            // parent
            base.links[Constants.LinkNameParent] = new Uri(
                urlHelper.RouteUrl(
                RouteNames.Database,
                new { dbName = this.Database },
                urlHelper.ActionContext.HttpContext.Request.Scheme
            ));

            // script
            this.Script = new Uri(
                urlHelper.RouteUrl(
                RouteNames.TableScript,
                new
                {
                    dbName = this.Database,
                    schemaName = this.Schema,
                    tableName = this.Name
                },
                urlHelper.ActionContext.HttpContext.Request.Scheme
            ));

            // columns
            this.Columns = new Uri(
                urlHelper.RouteUrl(
                RouteNames.TableColumns,
                new
                {
                    dbName = this.Database,
                    schemaName = this.Schema,
                    tableName = this.Name
                },
                urlHelper.ActionContext.HttpContext.Request.Scheme
            ));

            // top 100 rows
            this.Top100Rows = new Uri(
                urlHelper.RouteUrl(
                RouteNames.TableTop100Rows,
                new
                {
                    dbName = this.Database,
                    schemaName = this.Schema,
                    tableName = this.Name
                },
                urlHelper.ActionContext.HttpContext.Request.Scheme
            ));
        }
    }
}