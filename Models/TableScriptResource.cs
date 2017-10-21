using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;
using Serilog;
using Serilog.Events;
using SMO = Microsoft.SqlServer.Management.Smo;  
using SMOCommon = Microsoft.SqlServer.Management.Common;  

namespace MSSqlRestApi.Models
{
    public sealed class TableScriptResource : Resource
    {
        private string _scriptBody;
        public string ScriptBody { get { return this._scriptBody; } }
        private ServerContext _context;
        private string _dbName;
        private string _schemaName;
        private string _tableName;
        public TableScriptResource(ServerContext context, string dbName, string schemaName, string tableName, IUrlHelper urlHelper)
        {
            this._context = context;
            this._dbName = dbName;
            this._schemaName = schemaName;
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
                        "Database '{0}' not found. No T-SQL script generated.",
                        this._dbName);
                    Log.Warning(this._scriptBody);
                    return;
                }

                SMO.Table smoTable = smoDb.Tables[this._tableName, this._schemaName];
                if(smoTable == null)
                {
                    this._scriptBody = String.Format(
                        "Table '{0}' not found in Schema '{1}' in Database {2}. No T-SQL script generated.",
                        this._tableName, this._schemaName, this._dbName);
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
                    "Error generating script for Table '{0}' in Schema '{1}' in Database '{2}'\n\n{3}",
                    this._tableName, this._schemaName, this._dbName, e.ToString());
                Log.Error(this._scriptBody);
            }
        }

        public override void UpdateLinks(IUrlHelper urlHelper)
        {
            // self
            base.links[Constants.LinkNameSelf] = new Uri(
                urlHelper.RouteUrl(
                RouteNames.TableScript,
                new
                {
                    dbName = this._dbName,
                    schemaName = this._schemaName,
                    tableName = this._tableName
                },
                urlHelper.ActionContext.HttpContext.Request.Scheme
            ));

            // parent
            base.links[Constants.LinkNameParent] = new Uri(
                urlHelper.RouteUrl(
                RouteNames.Table,
                new
                {
                    dbName = this._dbName,
                    schemaName = this._schemaName,
                    tableName = this._tableName
                },
                urlHelper.ActionContext.HttpContext.Request.Scheme
            ));
        }
    }
}