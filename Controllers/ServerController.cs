using System;
using Microsoft.AspNetCore.Mvc;
using MSSqlRestApi.Models;

namespace MSSqlRestApi.Controllers
{
    [Route(Constants.RoutePathRoot)]
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