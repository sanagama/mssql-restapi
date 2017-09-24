using System;
using Microsoft.AspNetCore.Mvc;
using SMO = Microsoft.SqlServer.Management.Smo;  
using SMOCommon = Microsoft.SqlServer.Management.Common;  

namespace MSSqlWebapi.Models
{
    public sealed class DatabaseScriptResource : Resource
    {
        private string _scriptBody;
        public string ScriptBody { get { return this._scriptBody; } }
        private ServerContext _context;
        private string _dbName;
        public DatabaseScriptResource(ServerContext context, string dbName, IUrlHelper urlHelper)
        {
            this._context = context;
            this._dbName = dbName;
            this.UpdateLinks(urlHelper);
            this.GenerateScript();
        }

        private void GenerateScript()
        {
            this._scriptBody = String.Empty;
            try
            {
                SMO.Database smoDb = this._context.SmoServer.Databases[this._dbName];
                if (smoDb == null)
                {
                    this._scriptBody = String.Format(
                        "Database {0} not found. No T-SQL script generated.",
                        this._dbName);
                }
                else
                {
                    var generatedScript = String.Empty;
                    var scripter = new SMO.Scripter(this._context.SmoServer);
                    var options = new SMO.ScriptingOptions { ScriptSchema = true };
                    var scripts = smoDb.Script(options);
                    foreach (var script in scripts)
                        generatedScript += script;
                    this._scriptBody = generatedScript;
                }
            }
            catch(Exception e)
            {
                this._scriptBody = String.Format(
                    "Error while generating script for database {0}\n\n{1}",
                    this._dbName, e.ToString());
            }
        }
        public override void UpdateLinks(IUrlHelper urlHelper)
        {
            // self
            base.links[Constants.LinkNameSelf] = new Uri(
                urlHelper.RouteUrl(
                Constants.ApiRouteNameDatabaseScript,
                new { dbName = this._dbName },
                urlHelper.ActionContext.HttpContext.Request.Scheme
            ));

            // parent
            base.links[Constants.LinkNameParent] = new Uri(
                urlHelper.RouteUrl(
                Constants.ApiRouteNameDatabase,
                new { dbName = this._dbName },
                urlHelper.ActionContext.HttpContext.Request.Scheme
            ));
        }
    }
}