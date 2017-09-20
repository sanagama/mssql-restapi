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
    [Route("api/mssql/[controller]")]
    public class DatabasesController : Controller
    {
        private ServerContext _context;

        private DatabaseResource CreateDatabaseResource(SMO.Database db)
        {
            var dbResource = new DatabaseResource(db);
            dbResource.self = new Uri(@Url.Link("GetDatabase", new { dbName = dbResource.Name }));
            dbResource.parent = new Uri(@Url.Link("GetDatabases", null));
            dbResource.Tables = null;
            return dbResource;
        }

        public DatabasesController(ServerContext context)
        {
            _context = context;
        }

        // GET: api/mssql/databases
        //[HttpGet]
        [HttpGet(Name = "GetDatabases")]
        public IActionResult GetAll()
        {
            // Project a list DatabaseResource objects
            List<DatabaseResource> dbResources = new List<DatabaseResource>();
            foreach(SMO.Database db in _context.SmoServer.Databases)
            {
                dbResources.Add(CreateDatabaseResource(db));
            }

            var response = new 
            {
                self = new Uri(@Url.Link("GetDatabases", null)),
                parent = new Uri(@Url.Action("Get", "Root", null, @Url.ActionContext.HttpContext.Request.Scheme)),
                items = dbResources
            };
            return new ObjectResult(response);
        }

        // GET: api/mssql/databases/AdventureworksLT
        [HttpGet("{dbName}", Name = "GetDatabase")]
        public IActionResult GetByName(string dbName)
        {
            SMO.Database db = _context.SmoServer.Databases[dbName];
            if (db == null)
            {
                return NotFound();
            }
            else
            {
                return new ObjectResult(CreateDatabaseResource(db));
            }
        }

        // POST: api/mssql/databases
        [HttpPost]
        public IActionResult Post([FromBody] string value)
        {
            return BadRequest();
        }

        // PUT: api/mssql/databases
        [HttpPut]
        public IActionResult Put([FromBody]string value)
        {
            return BadRequest();
        }

        // DELETE: api/mssql/databases
        [HttpDelete]
        public IActionResult Delete()
        {
            return BadRequest();
        }
    }
}