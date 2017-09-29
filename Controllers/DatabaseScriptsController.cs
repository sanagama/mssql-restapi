using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SMO = Microsoft.SqlServer.Management.Smo;  
using SMOCommon = Microsoft.SqlServer.Management.Common;  
using MSSqlWebapi.Models;

namespace MSSqlWebapi.Controllers
{
    //[Route(Constants.ApiRoutePathDatabaseScript)]
    [Route("/api/mssql/databases/{dbName}/script")]
    public class DatabaseScriptsController : Controller
    {
        private ServerContext _context;

        public DatabaseScriptsController(ServerContext context)
        {
            this._context = context;
        }

        // Generate CREATE T-SQL script for Database
        // GET: api/mssql/{dbName}/script
        // GET: api/mssql/databases/AdventureworksLT/script
        //
        [HttpGet(Name = Constants.ApiRouteNameDatabaseScript)]
        public IActionResult GetDatabaseScript(string dbName)
        {
            DatabaseScriptResource resource = new DatabaseScriptResource(this._context, dbName, @Url);
            return Ok(resource);
        }

        // POST: api/mssql/{dbName}/script
        [HttpPost]
        public IActionResult Post([FromBody] string value)
        {
            return BadRequest();
        }

        // PUT: api/mssql/{dbName}/script
        [HttpPut]
        public IActionResult Put([FromBody]string value)
        {
            return BadRequest();
        }

        // DELETE: api/mssql/{dbName}/script
        [HttpDelete]
        public IActionResult Delete()
        {
            return BadRequest();
        }
    }
}