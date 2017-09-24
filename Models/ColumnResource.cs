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
        public ColumnResource(SMO.Column smoColumn, IUrlHelper urlHelper)
        {
            this._smoColumn = smoColumn;
            this._parent = this._smoColumn.Parent;
            this._smoTable = (SMO.Table) this._parent;  // TBD: only handling Table for now
            this.UpdateLinks(urlHelper);
        }

        public override void UpdateLinks(IUrlHelper urlHelper)
        {
            // self
            base.links[Constants.LinkNameSelf] = new Uri(
                urlHelper.RouteUrl(
                Constants.ApiRouteNameTableColumn,
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
                Constants.ApiRouteNameTable,
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