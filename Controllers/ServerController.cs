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
    [Route(Constants.ApiRoutePathRoot)]
    public class ServerController : Controller
    {
        private ServerContext _context;

        public ServerController(ServerContext context)
        {
            this._context = context;
        }

        // GET: api/mssql
        [HttpGet]
        [Route(Constants.ApiRoutePathRoot, Name = Constants.ApiRouteNameServer)]
        public IActionResult Get()
        {
            var resource = new ServerResource(this._context, @Url);
            return Ok(resource);
        }

        // POST: api/mssql
        [HttpPost]
        public IActionResult Post([FromBody] string value)
        {
            return BadRequest();
        }

        // PUT: api/mssql
        [HttpPut]
        public IActionResult Put([FromBody]string value)
        {
            return BadRequest();
        }

        // DELETE: api/mssql
        [HttpDelete]
        public IActionResult Delete()
        {
            return BadRequest();
        }
    }
}