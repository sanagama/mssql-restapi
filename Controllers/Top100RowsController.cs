using System;
using Microsoft.AspNetCore.Mvc;
using MSSqlWebapi.Models;

namespace MSSqlWebapi.Controllers
{
    [Route(Constants.RoutePathRoot + "/databases/{dbName}/tables/{schemaName}/{tableName}/top100rows")]
    public class Top100RowsController : Controller
    {
        private ServerContext _context;
        public Top100RowsController(ServerContext context)
        {
            this._context = context;
        }

        // Get Top 100 rows from table
        // GET: api/mssql/databases/{dbName}/tables/{schemaName}/{tableName}/Orders/top100rows
        // GET: api/mssql/databases/AdventureworksLT/tables/SalesLT/Orders/top100rows
        //
        [HttpGet(Name = RouteNames.TableTop100Rows)]
        public IActionResult GetTop100Rows(string dbName, string schemaName, string tableName)
        {
            return Ok(new Top100RowsResource(this._context, dbName, schemaName, tableName, @Url));
        }
    }
}