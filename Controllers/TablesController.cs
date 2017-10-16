using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SMO = Microsoft.SqlServer.Management.Smo;  
using SMOCommon = Microsoft.SqlServer.Management.Common;  
using MSSqlWebapi.Models;
using Serilog;
using Serilog.Events;

namespace MSSqlWebapi.Controllers
{
    [Route(RouteNames.Root + "/databases/{dbName}/tables")]
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
                Log.Warning("Database {0} not found. No Tables to display.", dbName);
                return NotFound();
            }

            // Project a list of TableResource objects
            smoDb.Tables.Refresh();
            List<TableResource> resources = new List<TableResource>();
            foreach(SMO.Table smoTable in smoDb.Tables)
            {
                TableResource resource = new TableResource(this._context, dbName, smoTable.Name, @Url);
                resources.Add(resource);
            }
            return Ok(resources);
        }

        // GET: api/mssql/databases/{dbName}/tables/{tableName}
        // GET: api/mssql/databases/AdventureworksLT/tables/Product
        [HttpGet("{tableName}", Name = RouteNames.Table)]
        public IActionResult GetTable(string dbName, string tableName)
        {
            // Project a TableResource object by name
            try
            {
                return Ok(new TableResource(this._context, dbName, tableName, @Url));
            }
            catch(Exception e)
            {
                Log.Warning(e.Message);
                return NotFound();
            }
        }
    }
}