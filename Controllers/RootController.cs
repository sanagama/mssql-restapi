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
    [Route("/api/mssql")]
    public class RootController : Controller
    {
        private ServerContext _context;

        private RootResource CreateRootResource(SMO.Server server)
        {
            var rootResource = new RootResource(server);
            rootResource.self = new Uri(@Url.Action("Get", "Root", null, @Url.ActionContext.HttpContext.Request.Scheme));
            rootResource.parent = null;
            rootResource.Databases = new Uri(@Url.Link("GetDatabases", null));
            return rootResource;
        }

        public RootController(ServerContext context)
        {
            _context = context;
        }

        // GET: api/mssql
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(CreateRootResource(_context.SmoServer));
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