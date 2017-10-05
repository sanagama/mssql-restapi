using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SMO = Microsoft.SqlServer.Management.Smo;  
using SMOCommon = Microsoft.SqlServer.Management.Common;  

namespace MSSqlWebapi.Models
{
    public sealed class DatabaseResource : Resource
    {
        public string Name { get { return this._smoDatabase.Name; } }
        public int Id { get { return this._smoDatabase.ID; } }
        public DateTime CreateDate { get { return this._smoDatabase.CreateDate; } }
        public Uri Script { get; set; }
        public Uri Tables { get; set; }
        private SMO.Database _smoDatabase;
        public DatabaseResource(SMO.Database smoDatabase, IUrlHelper urlHelper)
        {
            this._smoDatabase = smoDatabase;
            this._smoDatabase.Refresh();
            this.UpdateLinks(urlHelper);
        }

        public override void UpdateLinks(IUrlHelper urlHelper)
        {
            // self
            base.links[Constants.LinkNameSelf] = new Uri(
                urlHelper.RouteUrl(
                Constants.ApiRouteNameDatabase,
                new { dbName = this.Name },
                urlHelper.ActionContext.HttpContext.Request.Scheme
            ));

            // parent
            base.links[Constants.LinkNameParent] = new Uri(
                urlHelper.RouteUrl(
                Constants.ApiRouteNameServer,   // Route
                null,                           // route parameters
                urlHelper.ActionContext.HttpContext.Request.Scheme   // scheme
            ));

            // script
            this.Script = new Uri(
                urlHelper.RouteUrl(
                Constants.ApiRouteNameDatabaseScript,
                new { dbName = this.Name },
                urlHelper.ActionContext.HttpContext.Request.Scheme
            ));

            // tables
            this.Tables = new Uri(
                urlHelper.RouteUrl(
                Constants.ApiRouteNameTables,
                new { dbName = this.Name },
                urlHelper.ActionContext.HttpContext.Request.Scheme
            ));
        }
    }
}