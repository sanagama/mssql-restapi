using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SMO = Microsoft.SqlServer.Management.Smo;  
using SMOCommon = Microsoft.SqlServer.Management.Common;  
using MSSqlRestApi.Models;
using Serilog;
using Serilog.Events;

namespace MSSqlRestApi.Controllers
{
    [Route(Constants.RoutePathRoot + "/databases/{dbName}/tables")]
    public class TablesController : Controller
    {
        private ServerContext _context;

        public TablesController(ServerContext context)
        {
            this._context = context;
        }

        // GET: api/mssql/databases/{dbName}/tables
        // GET: api/mssql/databases/AdventureworksLT/tables
        [HttpGet(Name = RouteNames.Tables)]
        public IActionResult GetTables(string dbName)
        {
            SMO.Database smoDb = _context.SmoServer.Databases[dbName];
            if (smoDb == null)
            {
                Log.Warning("Database '{0}' not found. No Tables to display.", dbName);
                return NotFound();
            }

            // Get all tables in all schemas in this database
            smoDb.Tables.Refresh();
            var query = from table in smoDb.Tables.Cast<SMO.Table>()
//            where !table.IsSystemObject
            orderby table.Schema
            select table;

            // Group tables by schema names
            List<TablesInSchemaResource> resources = new List<TablesInSchemaResource>();
            var schemaGroups = query.ToLookup(t => t.Schema);
            
            // Project a list of TablesInSchemaResource objectsß
            foreach (var schemaGroup in schemaGroups)
            {
                // Get schema name and tables in schema name
                var schemaName = schemaGroup.Key;
                resources.Add(new TablesInSchemaResource(this._context, dbName, schemaName, @Url));
            }
            return Ok(resources);
        }

        // GET: api/mssql/databases/{dbName}/tables/{schemaName}
        // GET: api/mssql/databases/AdventureworksLT/tables/SalesLT
        [HttpGet("{schemaName}", Name = RouteNames.TablesInSchema)]
        public IActionResult GetTables(string dbName, string schemaName)
        {
            // Project a TableInSchemaResource object by schema name
            try
            {
                return Ok(new TablesInSchemaResource(this._context, dbName, schemaName, @Url));
            }
            catch(Exception e)
            {
                Log.Warning(e.Message);
                return NotFound();
            }
        }

        // GET: api/mssql/databases/{dbName}/tables/{schemaName}/{tableName}
        // GET: api/mssql/databases/AdventureworksLT/tables/SalesLT/Product
        [HttpGet("{schemaName}/{tableName}", Name = RouteNames.Table)]
        public IActionResult GetTable(string dbName, string schemaName, string tableName)
        {
            // Project a TableResource object by name
            try
            {
                return Ok(new TableResource(this._context, dbName, schemaName, tableName, @Url));
            }
            catch(Exception e)
            {
                Log.Warning(e.Message);
                return NotFound();
            }
        }
    }
}