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
    [Route(Constants.ApiRoutePathTables)]
    public class TablesController : Controller
    {
        private ServerContext _context;

        public TablesController(ServerContext context)
        {
            this._context = context;
        }

        // GET: api/mssql/databases/AdventureworksLT/tables
        [HttpGet]
        [Route(Constants.ApiRoutePathTables, Name = Constants.ApiRouteNameTables)]
        public IActionResult GetTables(string dbName)
        {
            SMO.Database smoDb = _context.SmoServer.Databases[dbName];
            if (smoDb == null)
            {
                return NotFound();
            }

            // Project a list of TableResource objects
            List<TableResource> resources = new List<TableResource>();
            foreach(SMO.Table smoTable in smoDb.Tables)
            {
                TableResource resource = new TableResource(smoTable, @Url);
                resources.Add(resource);
            }
            return Ok(resources);
        }

        // GET: api/mssql/databases/AdventureworksLT/tables/Product
        [HttpGet]
        [Route(Constants.ApiRoutePathTable, Name = Constants.ApiRouteNameTable)]
        public IActionResult GetTable(string dbName, string tableName)
        {
            SMO.Database smoDb = this._context.SmoServer.Databases[dbName];
            if (smoDb == null)
            {
                return NotFound();
            }

            SMO.Table smoTable = smoDb.Tables[tableName];
            if(smoTable == null)
            {
                return NotFound();
            }

            // Project a TableResource object
            TableResource resource = new TableResource(smoTable, @Url);
            return Ok(resource);
        }

        // POST: api/mssql/databases/AdventureworksLT/tables
        [HttpPost]
        public IActionResult Post([FromBody] string value)
        {
            return BadRequest();
        }

        // PUT: api/mssql/databases/AdventureworksLT/tables/Product
        [HttpPut]
        public IActionResult Put([FromBody]string value)
        {
            return BadRequest();
        }

        // DELETE: api/mssql/databases/AdventureworksLT/tables/Product
        [HttpDelete]
        public IActionResult Delete()
        {
            return BadRequest();
        }
    }
}