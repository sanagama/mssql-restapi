using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SMO = Microsoft.SqlServer.Management.Smo;  
using SMOCommon = Microsoft.SqlServer.Management.Common;  

namespace MSSqlWebapi.Models
{
    public sealed class TableResource : Resource
    {        
        public string Name { get { return this._smoTable.Name; } }
        public int Id { get { return this._smoTable.ID; } }
        public DateTime CreateDate { get { return this._smoTable.CreateDate; } }
        public long RowCount { get { return this._smoTable.RowCount; } }
        public Uri Script { get; set; }
        public Uri Columns { get; set; }
        private SMO.Table _smoTable;
        public TableResource(SMO.Table smoTable, IUrlHelper urlHelper)
        {
            this._smoTable = smoTable;
            this.UpdateLinks(urlHelper);
        }

        public override void UpdateLinks(IUrlHelper urlHelper)
        {
            // tables
            base.links[Constants.LinkNameSelf] = new Uri(
                urlHelper.RouteUrl(
                Constants.ApiRouteNameTable,   // Route
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
                Constants.ApiRouteNameDatabases,    // Route
                new { dbName = this._smoTable.Parent.Name },         // route parameters
                urlHelper.ActionContext.HttpContext.Request.Scheme   // scheme
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

/*
            // rows
            this.Tables = new Uri(
                urlHelper.RouteUrl(
                Constants.ApiRouteNameScript,   // Route
                new { dbName = this.Name },     // route parameters
                urlHelper.ActionContext.HttpContext.Request.Scheme   // scheme
            ));
*/
        }
    }
}