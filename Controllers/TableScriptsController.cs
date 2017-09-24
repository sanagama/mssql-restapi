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
    [Route(Constants.ApiRoutePathTableScript)]
    public class TableScriptsController : Controller
    {
        private ServerContext _context;
        public TableScriptsController(ServerContext context)
        {
            this._context = context;
        }

        // Generate CREATE T-SQL script for Table
        // GET: api/mssql/{dbName}/{tableName}/script
        // GET: api/mssql/databases/AdventureworksLT/Orders/script
        //
        [HttpGet]
        [Route(Constants.ApiRoutePathTableScript, Name = Constants.ApiRouteNameTableScript)]
        public IActionResult GetTableScript(string dbName, string tableName)
        {
            TableScriptResource resource = new TableScriptResource(this._context, dbName, tableName, @Url);
            return Ok(resource);
        }

        // POST: api/mssql/{dbName}/{tableName}/script
        [HttpPost]
        public IActionResult Post([FromBody] string value)
        {
            return BadRequest();
        }

        // PUT: api/mssql/{dbName}/{tableName}/script
        [HttpPut]
        public IActionResult Put([FromBody]string value)
        {
            return BadRequest();
        }

        // DELETE: api/mssql/{dbName}/{tableName}/script
        [HttpDelete]
        public IActionResult Delete()
        {
            return BadRequest();
        }
    }
}