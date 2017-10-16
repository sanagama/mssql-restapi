using System;
using Microsoft.AspNetCore.Mvc;
using MSSqlWebapi.Models;

namespace MSSqlWebapi.Controllers
{
    [Route(RouteNames.Root)]
    public class ServerController : Controller
    {
        private ServerContext _context;

        public ServerController(ServerContext context)
        {
            this._context = context;
        }

        // GET: api/mssql
        [HttpGet(Name = RouteNames.Server)]
        public IActionResult Get()
        {
            return Ok(new ServerResource(this._context, @Url));
        }
    }
}