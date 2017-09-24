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
    public class Top100RowsController : Controller
    {
        private ServerContext _context;
        public Top100RowsController(ServerContext context)
        {
            this._context = context;
        }

        // Generate CREATE T-SQL script for Table
        // GET: api/mssql/databases/{dbName}/{tableName}/top100rows
        // GET: api/mssql/databases/AdventureworksLT/Orders/top100rows
        //
        [HttpGet]
        [Route(Constants.ApiRoutePathTableTop100Rows, Name = Constants.ApiRouteNameTableTop100Rows)]
        public IActionResult GetTableScript(string dbName, string tableName)
        {
            Top100RowsResource resource = new Top100RowsResource(this._context, dbName, tableName, @Url);
            return Ok(resource);
        }

        // POST: api/mssql/databases/{dbName}/{tableName}/top100rows
        [HttpPost]
        public IActionResult Post([FromBody] string value)
        {
            return BadRequest();
        }

        // PUT: api/mssql/databases/{dbName}/{tableName}/top100rows
        [HttpPut]
        public IActionResult Put([FromBody]string value)
        {
            return BadRequest();
        }

        // DELETE: api/mssql/databases/{dbName}/{tableName}/top100rows
        [HttpDelete]
        public IActionResult Delete()
        {
            return BadRequest();
        }
    }
}