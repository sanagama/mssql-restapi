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
    [Route(Constants.RoutePathRoot + "/databases/{dbName}/tables/{schemaName}/{tableName}/columns")]
    public class ColumnsController : Controller
    {
        private ServerContext _context;

        public ColumnsController(ServerContext context)
        {
            this._context = context;
        }

        // GET: api/mssql/databases/{dbName}/tables/{schemaName}/{tableName}/columns
        // GET: api/mssql/databases/AdventureworksLT/tables/SalesLT/Product/columns
        [HttpGet(Name = RouteNames.TableColumns)]
        public IActionResult GetColumns(string dbName, string schemaName, string tableName)
        {
            SMO.Database smoDb = _context.SmoServer.Databases[dbName];
            if (smoDb == null)
            {
                Log.Warning("Database '{0}' not found. No Columns to display.", dbName);
                return NotFound();
            }

            SMO.Table smoTable = smoDb.Tables[tableName, schemaName];
            if (smoTable == null)
            {
                Log.Warning("Table '{0}' not found in Schema '{1}' in Database {2}. No Columns to display.", tableName, schemaName, dbName);
                return NotFound();
            }

            // Project a list of ColumnResource objects
            smoTable.Columns.Refresh();
            List<ColumnResource> resources = new List<ColumnResource>();
            foreach(SMO.Column smoColumn in smoTable.Columns)
            {
                resources.Add(new ColumnResource(this._context, dbName, schemaName, tableName, smoColumn.Name, @Url));
            }
            return Ok(resources);
        }

        // GET: api/mssql/databases/{dbName}/tables/{schemaName}/{tableName}/columns/{columnName}
        // GET: api/mssql/databases/AdventureworksLT/tables/SalesLT/Product/columns/{columnName}
        [HttpGet("{columnName}", Name = RouteNames.TableColumn)]
        public IActionResult GetColumn(string dbName, string schemaName, string tableName, string columnName)
        {
            // Project a ColumnResource object by name
            try
            {
                return Ok(new ColumnResource(this._context, dbName, schemaName, tableName, columnName, @Url));
            }
            catch(Exception e)
            {
                Log.Warning(e.Message);
                return NotFound();
            }
        }
    }
}