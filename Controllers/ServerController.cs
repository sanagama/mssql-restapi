using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SMO = Microsoft.SqlServer.Management.Smo;  
using SMOCommon = Microsoft.SqlServer.Management.Common;  
using MSSqlWebapi.Models;
using Microsoft.AspNetCore.Mvc.Routing;

namespace MSSqlWebapi.Controllers
{
    [Route(Constants.ApiRouteRoot)]
    public class ServerController : Controller
    {
        private ServerContext _context;

/*
        private ServerResource CreateServerResource()
        {
            serverResource.self = new Uri(
                @Url.RouteUrl(
                    Constants.ApiRouteNameServer,   // Route
                    null,                           // route parameters
                    @Url.ActionContext.HttpContext.Request.Scheme   // scheme
            ));

            serverResource.Databases = new Uri(
                @Url.RouteUrl(
                    Constants.ApiRouteNameDatabases,    // Route
                    null,                               // route parameters
                    @Url.ActionContext.HttpContext.Request.Scheme   // scheme
            ));

            serverResource.Databases = new Uri(
                @Url.Action(
                    "GetDatabases", // the Name in [HttpXXX] method
                    "Databases",     // the controller name 
                    null,           // route parameters
                    @Url.ActionContext.HttpContext.Request.Scheme   // scheme
                ));

            return serverResource;
        }
 */

        public ServerController(ServerContext context)
        {
            this._context = context;
        }

        // GET: api/mssql
        [HttpGet]
        [Route(Constants.ApiRouteRoot, Name = Constants.ApiRouteNameServer)]
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