using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MSSqlWebapi.Models;

namespace MSSqlWebapi.Controllers
{
    [Route("api/mssql")]
    public class ServerController : Controller
    {
        private ServerContext _context;

        public ServerController(ServerContext context)
        {
            _context = context;
        }

        // GET mssql
        [HttpGet(Name = nameof(GetRoot))]
        public IActionResult GetRoot()
        {
            var response = new ServerResource(_context.SmoServer)
            {
                Self = Link.To(nameof(GetRoot)),
                Databases = null
            };

            return Ok(response);

        }

        // POST mssql
        [HttpPost]
        public IActionResult Post([FromBody] string value)
        {
            return BadRequest();
        }

        // PUT mssql
        [HttpPut]
        public IActionResult Put([FromBody]string value)
        {
            return BadRequest();
        }

        // DELETE mssql
        [HttpDelete]
        public IActionResult Delete()
        {
            return BadRequest();
        }
    }
}