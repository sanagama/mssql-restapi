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
    [Route(RouteNames.Root + "/databases/{dbName}/tables/{tableName}/columns")]
    public class ColumnsController : Controller
    {
        private ServerContext _context;

        public ColumnsController(ServerContext context)
        {
            this._context = context;
        }

        // GET: api/mssql/databases/{dbName}/tables/{tableName}/columns
        // GET: api/mssql/databases/AdventureworksLT/tables/Product/columns
        [HttpGet(Name = RouteNames.TableColumns)]
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
                ColumnResource resource = new ColumnResource(this._context, dbName, tableName, smoColumn.Name, @Url);
                resources.Add(resource);
            }
            return Ok(resources);
        }

        // GET: api/mssql/databases/{dbName}/tables/{tableName}/columns/{columnName}
        // GET: api/mssql/databases/AdventureworksLT/tables/Product/columns/{columnName}
        [HttpGet("{columnName}", Name = RouteNames.TableColumn)]
        public IActionResult GetColumn(string dbName, string tableName, string columnName)
        {
            // Project a ColumnResource object by name
            try
            {
                return Ok(new ColumnResource(this._context, dbName, tableName, columnName, @Url));
            }
            catch(Exception e)
            {
                Log.Warning(e.Message);
                return NotFound();
            }
        }
    }
}