using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SMO = Microsoft.SqlServer.Management.Smo;  
using SMOCommon = Microsoft.SqlServer.Management.Common;  
using Serilog;
using Serilog.Events;

namespace MSSqlWebapi.Models
{
    public sealed class TableResource : Resource
    {        
        public string Name { get { return this._smoTable.Name; } }
        public int Id { get { return this._smoTable.ID; } }
        public string Schema { get { return this._smoTable.Schema; } }
        public DateTime CreateDate { get { return this._smoTable.CreateDate; } }
        public long RowCount { get { return this._smoTable.RowCount; } }
        public long ColumnCount { get { return this._smoTable.Columns.Count; } }
        public Uri Script { get; set; }
        public Uri Columns { get; set; }
        public Uri Top100Rows { get; set; }
        private ServerContext _context;
        private SMO.Table _smoTable;
        private SMO.Server SmoServer { get { return this._context.SmoServer; } }        
        private SMO.Table SmoTable { get { return this._smoTable; } }        
        
        public TableResource(ServerContext context, string dbName, string tableName, IUrlHelper urlHelper)
        {
            this._context = context;

            // Get database by name
            this.SmoServer.Databases.Refresh();
            SMO.Database smoDb = this.SmoServer.Databases[dbName];
            if (smoDb == null)
            {
                throw new SMO.SmoException(String.Format("Database {0} not found.", dbName));
            }

            // Get table by name
            smoDb.Tables.Refresh();
            this._smoTable = smoDb.Tables[tableName];
            if(this._smoTable == null)
            {
                throw new SMO.SmoException(String.Format("Table {0} not found in Database {1}.", tableName, dbName));
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
                    dbName = this.SmoTable.Parent.Name,
                    tableName = this.SmoTable.Name
                },
                urlHelper.ActionContext.HttpContext.Request.Scheme   // scheme
            ));

            // parent
            base.links[Constants.LinkNameParent] = new Uri(
                urlHelper.RouteUrl(
                RouteNames.Database,
                new { dbName = this.SmoTable.Parent.Name },
                urlHelper.ActionContext.HttpContext.Request.Scheme
            ));

            // script
            this.Script = new Uri(
                urlHelper.RouteUrl(
                RouteNames.TableScript,
                new
                {
                    dbName = this.SmoTable.Parent.Name,
                    tableName = this.SmoTable.Name
                },
                urlHelper.ActionContext.HttpContext.Request.Scheme
            ));

            // columns
            this.Columns = new Uri(
                urlHelper.RouteUrl(
                RouteNames.TableColumns,
                new
                {
                    dbName = this.SmoTable.Parent.Name,
                    tableName = this.SmoTable.Name
                },
                urlHelper.ActionContext.HttpContext.Request.Scheme
            ));

            // top 100 rows
            this.Top100Rows = new Uri(
                urlHelper.RouteUrl(
                RouteNames.TableTop100Rows,
                new
                {
                    dbName = this.SmoTable.Parent.Name,
                    tableName = this.SmoTable.Name
                },
                urlHelper.ActionContext.HttpContext.Request.Scheme
            ));
        }
    }
}