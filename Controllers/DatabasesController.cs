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
    [Route("/api/mssql/[controller]")]
    public class DatabasesController : Controller
    {
        private ServerContext _context;

        public DatabasesController(ServerContext context)
        {
            this._context = context;
        }

        // GET: api/mssql/databases
        [HttpGet(Name = Constants.ApiRouteNameDatabases)]
        public IActionResult GetDatabases()
        {
            // Project a list of DatabaseResource objects
            List<DatabaseResource> resources = new List<DatabaseResource>();
            foreach(SMO.Database smoDb in this._context.SmoServer.Databases)
            {
                var resource = new DatabaseResource(smoDb, @Url);
                resources.Add(resource);
            }
            return Ok(resources);
        }

        // GET: api/mssql/databases/AdventureworksLT
        [HttpGet("{dbName}", Name = Constants.ApiRouteNameDatabase)]
        public IActionResult GetDatabase(string dbName)
        {
            SMO.Database smoDb = this._context.SmoServer.Databases[dbName];
            if (smoDb == null)
            {
                return NotFound();
            }

            // Project a DatabaseResource object
            var resource = new DatabaseResource(smoDb, @Url);
            return Ok(resource);
        }

        // POST: api/mssql/databases
        [HttpPost]
        public IActionResult Post([FromBody] string value)
        {
            return BadRequest();
        }

        // PUT: api/mssql/databases/AdventureworksLT
        [HttpPut]
        public IActionResult Put([FromBody]string value)
        {
            return BadRequest();
        }

        // DELETE: api/mssql/databases/AdventureworksLT
        [HttpDelete]
        public IActionResult Delete()
        {
            return BadRequest();
        }
    }
}