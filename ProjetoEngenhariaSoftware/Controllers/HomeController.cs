using ProjetoEngenhariaSoftware.Helpers;
using ProjetoEngenhariaSoftware.Models;
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
        [SwaggerResponse(HttpStatusCode.OK, "Return a list of pets", typeof(List<PetClass>))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Custom exception indicating that api token is not valid or has not been reported", typeof(Exception))]
        public IHttpActionResult GetAllPets()
        {
            return Content(HttpStatusCode.OK, Banco.selectAll());
        }

        [HttpGet]
        [AllowAnonymous]
        [SwaggerResponse(HttpStatusCode.OK, "Return a list of pets", typeof(List<PetClass>))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Custom exception indicating that api token is not valid or has not been reported", typeof(Exception))]
        public IHttpActionResult GetByID(int id)
        {
            return Content(HttpStatusCode.OK, Banco.select(id));
        }

        [HttpPost]
        [AllowAnonymous]
        [SwaggerResponse(HttpStatusCode.OK, "Return the id from this pet", typeof(int))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Custom exception indicating that api token is not valid or has not been reported", typeof(Exception))]
        public IHttpActionResult InsertPets(PetClass Pet)
        {
            return Content(HttpStatusCode.OK, Banco.insert(Pet));
        }

        [HttpPut]
        [AllowAnonymous]
        [SwaggerResponse(HttpStatusCode.OK, "Return a confirmation to update", typeof(bool))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Custom exception indicating that api token is not valid or has not been reported", typeof(Exception))]
        public IHttpActionResult UpdatePets([FromUri]int id, [FromBody] PetClass pet)
        {
            return Content(HttpStatusCode.OK, Banco.update(pet,id));
        }

        [HttpDelete]
        [AllowAnonymous]
        [SwaggerResponse(HttpStatusCode.OK, "Return a list of pets", typeof(bool))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Custom exception indicating that api token is not valid or has not been reported", typeof(Exception))]
        public IHttpActionResult DeletePet(int id)
        {
            try
            {
                Banco.delete(id);
                return Content(HttpStatusCode.OK, true);
            }
            catch
            {
                return Content(HttpStatusCode.OK, false);
            }
        }
    }
}