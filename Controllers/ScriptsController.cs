using System;
using Microsoft.AspNetCore.Mvc;
using MSSqlWebapi.Models;

namespace MSSqlWebapi.Controllers
{
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
        [HttpGet(RouteNames.Root + "/databases/{dbName}/script", Name = RouteNames.DatabaseScript)]
        public IActionResult GetDatabaseScript(string dbName)
        {
            return Ok(new DatabaseScriptResource(this._context, dbName, @Url));
        }

        // Generate CREATE T-SQL script for Table
        // GET: api/mssql/databases/{dbName}/tables/{tableName}/script
        // GET: api/mssql/databases/AdventureworksLT/tables/Orders/script
        //
        [HttpGet(RouteNames.Root + "/databases/{dbName}/tables/{tableName}/script", Name = RouteNames.TableScript)]
        public IActionResult GetTableScript(string dbName, string tableName)
        {
            return Ok(new TableScriptResource(this._context, dbName, tableName, @Url));
        }
    }
}