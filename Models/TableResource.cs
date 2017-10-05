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
        private SMO.Table _smoTable;
        public TableResource(SMO.Table smoTable, IUrlHelper urlHelper)
        {
            this._smoTable = smoTable;
            this.UpdateLinks(urlHelper);
        }
        public override void UpdateLinks(IUrlHelper urlHelper)
        {
            // self
            base.links[Constants.LinkNameSelf] = new Uri(
                urlHelper.RouteUrl(
                Constants.ApiRouteNameTable,
                new
                {
                    dbName = this._smoTable.Parent.Name,
                    tableName = this._smoTable.Name
                },
                urlHelper.ActionContext.HttpContext.Request.Scheme   // scheme
            ));

            // parent
            base.links[Constants.LinkNameParent] = new Uri(
                urlHelper.RouteUrl(
                Constants.ApiRouteNameDatabase,
                new { dbName = this._smoTable.Parent.Name },
                urlHelper.ActionContext.HttpContext.Request.Scheme
            ));

            // script
            this.Script = new Uri(
                urlHelper.RouteUrl(
                Constants.ApiRouteNameTableScript,
                new
                {
                    dbName = this._smoTable.Parent.Name,
                    tableName = this._smoTable.Name
                },
                urlHelper.ActionContext.HttpContext.Request.Scheme
            ));

            // columns
            this.Columns = new Uri(
                urlHelper.RouteUrl(
                Constants.ApiRouteNameTableColumns,
                new
                {
                    dbName = this._smoTable.Parent.Name,
                    tableName = this._smoTable.Name
                },
                urlHelper.ActionContext.HttpContext.Request.Scheme
            ));

            // top 100 rows
            this.Top100Rows = new Uri(
                urlHelper.RouteUrl(
                Constants.ApiRouteNameTableTop100Rows,
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