using System;
//using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Events;
using System.Collections.Specialized;
using SMO = Microsoft.SqlServer.Management.Smo;  
using SMOCommon = Microsoft.SqlServer.Management.Common;  

namespace MSSqlRestApi.Models
{
    public sealed class ColumnResource : Resource
    {
        public string Name { get { return this._smoColumn.Name; } }
        public int Id { get { return this._smoColumn.ID; } }
        public string DataType { get { return this._smoColumn.DataType.ToString(); } }
        public string Default { get { return this._smoColumn.Default.ToString(); } }

        private SMO.Column _smoColumn;
        private SMO.SqlSmoObject _parent;
        private SMO.Table _smoTable;
        private ServerContext _context;

        public ColumnResource(ServerContext context, string dbName, string schemaName, string tableName, string columnName, IUrlHelper urlHelper)
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
            SMO.Table smoTable = smoDb.Tables[tableName, schemaName];
            if(smoTable == null)
            {
                throw new SMO.SmoException(String.Format("Table '{0}' not found in Schema '{1}' in Database '{1}'.",
                    tableName, schemaName, dbName));
            }

            // Get column by name
            smoTable.Columns.Refresh();
            this._smoColumn = smoTable.Columns[columnName];
            if(this._smoColumn == null)
            {
                throw new SMO.SmoException(String.Format("Column {0} not found in Table {1} not found in Database {2}.", columnName, tableName, dbName));
            }
            
            this._parent = this._smoColumn.Parent;
            this._smoTable = (SMO.Table) this._parent;  // TODO: handle other types of parents (only handling Table for now)
            this.UpdateLinks(urlHelper);
        }

        public override void UpdateLinks(IUrlHelper urlHelper)
        {
            // self
            base.links[Constants.LinkNameSelf] = new Uri(
                urlHelper.RouteUrl(
                RouteNames.TableColumn,
                new
                {
                    dbName = this._smoTable.Parent.Name,
                    tableName = this._smoTable.Name,
                    schemaName = this._smoTable.Schema,
                    columnName = this.Name
                },
                urlHelper.ActionContext.HttpContext.Request.Scheme
            ));

            // parent
            base.links[Constants.LinkNameParent] = new Uri(
                urlHelper.RouteUrl(
                RouteNames.Table,
                new
                {
                    dbName = this._smoTable.Parent.Name,
                    schemaName = this._smoTable.Schema,
                    tableName = this._smoTable.Name
                },
                urlHelper.ActionContext.HttpContext.Request.Scheme
            ));
        }        
    }
}