using System;
using Microsoft.AspNetCore.Mvc;
using MSSqlRestApi.Models;

namespace MSSqlRestApi.Controllers
{
    [Route(Constants.RoutePathRoot + "/databases/{dbName}")]
    public class ScriptsController : Controller
    {
        private ServerContext _context;

        public ScriptsController(ServerContext context)
        {
            this._context = context;
        }

        // Generate CREATE T-SQL script for Database
        // GET: api/mssql/databases/{dbName}/script
        // GET: api/mssql/databases/AdventureworksLT/script
        //
        [HttpGet("script", Name = RouteNames.DatabaseScript)]
        public IActionResult GetDatabaseScript(string dbName)
        {
            return Ok(new DatabaseScriptResource(this._context, dbName, @Url));
        }

        // Generate CREATE T-SQL script for Table
        // GET: api/mssql/databases/{dbName}/tables/{schemaName}/{tableName}/script
        // GET: api/mssql/databases/AdventureworksLT/tables/SalesLT/Orders/script
        //
        [HttpGet("tables/{schemaName}/{tableName}/script", Name = RouteNames.TableScript)]
        public IActionResult GetTableScript(string dbName, string schemaName, string tableName)
        {
            return Ok(new TableScriptResource(this._context, dbName, schemaName, tableName, @Url));
        }
    }
}