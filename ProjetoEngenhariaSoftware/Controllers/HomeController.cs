using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace ProjetoEngenhariaSoftware.Controllers
{
    public class HomeController : ApiController
    {
        // <summary>
        /// Return the access token
        /// </summary>
        /// <param name="ObjUsuario">authentication data</param>
        /// <returns>User</returns>
        /// <remarks>AccessLevel is not a required</remarks>
        [HttpGet]
        [AllowAnonymous]
        [SwaggerResponse(HttpStatusCode.OK, "Return a list of pets", typeof(string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Exception Custom Object with message about error in ParkingException.Message", typeof(Exception))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Custom exception indicating that api token is not valid or has not been reported", typeof(Exception))]
        public IHttpActionResult GetPets()
        {
            return Content(HttpStatusCode.OK, "pets");
        }
    }
}