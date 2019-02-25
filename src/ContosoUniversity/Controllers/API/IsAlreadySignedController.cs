using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ContosoUniversity.BusinessLayer;
using ContosoUniversity.DAL;
using ContosoUniversity.Models;

namespace ContosoUniversity.Controllers.API
{    
    [RoutePrefix("API/IsAlreadySigned")]
    public class IsAlreadySignedController : ApiController
    {
        public SchoolContext db = new SchoolContext();
        [Route("{username}")]
        [HttpGet]
        public IHttpActionResult getUsername(string username)
        {
            UsernameAvailable user = new UsernameAvailable(); 
            var result = user.UsernameIsAvailable(username);
            return Ok(result);
        }
    }
}

 