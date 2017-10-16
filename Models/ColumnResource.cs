using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SMO = Microsoft.SqlServer.Management.Smo;  
using SMOCommon = Microsoft.SqlServer.Management.Common;  

namespace MSSqlWebapi.Models
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
        private SMO.Server SmoServer { get { return this._context.SmoServer; } }        

        public ColumnResource(ServerContext context, string dbName, string tableName, string columnName, IUrlHelper urlHelper)
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
            SMO.Table smoTable = smoDb.Tables[tableName];
            if(smoTable == null)
            {
                throw new SMO.SmoException(String.Format("Table {0} not found in Database {1}.", tableName, dbName));
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
                    columnName = this._smoColumn.Name
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
                    tableName = this._smoTable.Name
                },
                urlHelper.ActionContext.HttpContext.Request.Scheme
            ));
        }        
    }
}