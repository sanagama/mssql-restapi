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
    // See: https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/routing
    [Route(RouteNames.Root + "/databases")]
    public class DatabasesController : Controller
    {
        private ServerContext _context;

        public DatabasesController(ServerContext context)
        {
            this._context = context;
        }

        // GET: api/mssql/databases
        [HttpGet(Name = RouteNames.Databases)]
        public IActionResult GetDatabases()
        {
            // Project a list of DatabaseResource objects
            this._context.SmoServer.Databases.Refresh();
            List<DatabaseResource> resources = new List<DatabaseResource>();
            foreach(SMO.Database smoDb in this._context.SmoServer.Databases)
            {
                var resource = new DatabaseResource(this._context, smoDb.Name, @Url);
                resources.Add(resource);
            }
            return Ok(resources);
        }

        // GET: api/mssql/databases/AdventureworksLT
        [HttpGet("{dbName}", Name = RouteNames.Database)]
        public IActionResult GetDatabase(string dbName)
        {
            // Project a DatabaseResource object by name
            try
            {
                return Ok(new DatabaseResource(this._context, dbName, @Url));
            }
            catch(Exception e)
            {
                Log.Warning(e.Message);
                return NotFound();
            }
        }
    }
}