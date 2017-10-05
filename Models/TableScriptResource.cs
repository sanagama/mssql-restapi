using System;
using Microsoft.AspNetCore.Mvc;
using SMO = Microsoft.SqlServer.Management.Smo;  
using SMOCommon = Microsoft.SqlServer.Management.Common;  
using Serilog;
using Serilog.Events;
using System.Collections.Specialized;

namespace MSSqlWebapi.Models
{
    public sealed class TableScriptResource : Resource
    {
        private string _scriptBody;
        public string ScriptBody { get { return this._scriptBody; } }
        private ServerContext _context;
        private string _dbName;
        private string _tableName;
        public TableScriptResource(ServerContext context, string dbName, string tableName, IUrlHelper urlHelper)
        {
            this._context = context;
            this._dbName = dbName;
            this._tableName = tableName;
            this.UpdateLinks(urlHelper);
            this.GenerateScript();
        }

        private void GenerateScript()
        {
            this._scriptBody = String.Empty;
            try
            {
                SMO.Database smoDb = _context.SmoServer.Databases[this._dbName];
                if (smoDb == null)
                {
                    this._scriptBody = String.Format(
                        "Database {0} not found. No T-SQL script generated.",
                        this._dbName);
                    Log.Warning(this._scriptBody);
                    return;
                }

                SMO.Table smoTable = smoDb.Tables[this._tableName];
                if(smoTable == null)
                {
                    this._scriptBody = String.Format(
                        "Table {0} not found in Database {1}. No T-SQL script generated.",
                        this._tableName, this._dbName);
                    Log.Warning(this._scriptBody);
                    return;
                }

                StringCollection scripts = smoTable.Script();
                foreach (var script in scripts)
                    this._scriptBody += script;
            }
            catch(Exception e)
            {
                this._scriptBody = String.Format(
                    "Error while generating script for table {0} in database {1}\n\n{2}",
                    this._tableName, this._dbName, e.ToString());
                Log.Error(this._scriptBody);
            }
        }

        public override void UpdateLinks(IUrlHelper urlHelper)
        {
            // self
            base.links[Constants.LinkNameSelf] = new Uri(
                urlHelper.RouteUrl(
                Constants.ApiRouteNameTableScript,
                new
                {
                    dbName = this._dbName,
                    tableName = this._tableName
                },
                urlHelper.ActionContext.HttpContext.Request.Scheme
            ));

            // parent
            base.links[Constants.LinkNameParent] = new Uri(
                urlHelper.RouteUrl(
                Constants.ApiRouteNameTable,
                new
                {
                    dbName = this._dbName,
                    tableName = this._tableName
                },
                urlHelper.ActionContext.HttpContext.Request.Scheme
            ));
        }
    }
}