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
    [Route("/api/mssql/databases/{dbName}/tables/{tableName}/[controller]")]
    public class ColumnsController : Controller
    {
        private ServerContext _context;

        public ColumnsController(ServerContext context)
        {
            this._context = context;
        }

        // GET: api/mssql/databases/AdventureworksLT/tables/Product/columns
        [HttpGet(Name = Constants.ApiRouteNameTableColumns)]
        public IActionResult GetColumns(string dbName, string tableName)
        {
            SMO.Database smoDb = _context.SmoServer.Databases[dbName];
            if (smoDb == null)
            {
                Log.Warning("Database {0} not found. No Columns to display.", dbName);
                return NotFound();
            }

            SMO.Table smoTable = smoDb.Tables[tableName];
            if (smoTable == null)
            {
                Log.Warning("Table {0} not found in Database {1}. No Columns to display.", tableName, dbName);
                return NotFound();
            }

            // Project a list of ColumnResource objects
            smoTable.Columns.Refresh();
            List<ColumnResource> resources = new List<ColumnResource>();
            foreach(SMO.Column smoColumn in smoTable.Columns)
            {
                ColumnResource resource = new ColumnResource(smoColumn, @Url);
                resources.Add(resource);
            }
            return Ok(resources);
        }

        // GET: api/mssql/databases/AdventureworksLT/tables/Product/columns/{columnName}
        [HttpGet("{columnName}", Name = Constants.ApiRouteNameTableColumn)]
        public IActionResult GetColumn(string dbName, string tableName, string columnName)
        {
            SMO.Database smoDb = this._context.SmoServer.Databases[dbName];
            if (smoDb == null)
            {
                Log.Warning("Database {0} not found. No Columns to display.", dbName);
                return NotFound();
            }

            SMO.Table smoTable = smoDb.Tables[tableName];
            if(smoTable == null)
            {
                Log.Warning("Table {0} not found in Database {1}. No Columns to display.", tableName, dbName);
                return NotFound();
            }

            SMO.Column smoColumn = smoTable.Columns[columnName];
            if(smoColumn == null)
            {
                Log.Warning("Column {0} not found in Table {1} in Database {2}.", columnName, tableName, dbName);
                return NotFound();
            }

            // Project a ColumnResource object
            ColumnResource resource = new ColumnResource(smoColumn, @Url);
            return Ok(resource);
        }

        // POST: api/mssql/databases/AdventureworksLT/tables/Product/columns
        [HttpPost]
        public IActionResult Post([FromBody] string value)
        {
            return BadRequest();
        }

        // PUT: api/mssql/databases/AdventureworksLT/tables/Product/columns/{columnName}
        [HttpPut]
        public IActionResult Put([FromBody]string value)
        {
            return BadRequest();
        }

        // DELETE: api/mssql/databases/AdventureworksLT/tables/Product/columns/{columnName}
        [HttpDelete]
        public IActionResult Delete()
        {
            return BadRequest();
        }
    }
}