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
    public class PetController : ApiController
    {
        /// <summary>
        /// Return a list of pets
        /// </summary>
        /// <returns>Return a list of pets</returns>
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, "Return a list of pets", typeof(List<PetClass>))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Custom exception indicating that api token is not valid or has not been reported", typeof(Exception))]
        [AllowAnonymous]

        public IHttpActionResult GetAllPets()
        {
            return Content(HttpStatusCode.OK, BancoPet.selectAll());
        }

        /// <summary>
        /// Return a list of pets
        /// </summary>
        /// <param name="id"> a id for a PetClass list</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [SwaggerResponse(HttpStatusCode.OK, "Return a list of pets", typeof(List<PetClass>))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Custom exception indicating that api token is not valid or has not been reported", typeof(Exception))]
        public IHttpActionResult GetByID(int id)
        {
            return Content(HttpStatusCode.OK, Banco.select(id));
        }

        /// <summary>
        /// insert a pet in list 
        /// </summary>
        /// <param name="Pet">object</param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, "Return the id from this pet", typeof(int))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Custom exception indicating that api token is not valid or has not been reported", typeof(Exception))]
        [AllowAnonymous]

        public IHttpActionResult InsertPets(PetClass Pet)
        {
            return Content(HttpStatusCode.OK, BancoPet.insert(Pet));
        }

        /// <summary>
        /// update a pet from a list 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pet"></param>
        /// <returns></returns>
        [HttpPut]
        [AllowAnonymous]
        [SwaggerResponse(HttpStatusCode.OK, "Return a confirmation to update", typeof(bool))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Custom exception indicating that api token is not valid or has not been reported", typeof(Exception))]
        public IHttpActionResult UpdatePets([FromUri]int id, [FromBody] PetClass pet)
        {
            return Content(HttpStatusCode.OK, BancoPet.update(pet,id));
        }

        /// <summary>
        /// delete pet from a list
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [AllowAnonymous]
        [SwaggerResponse(HttpStatusCode.OK, "Return a list of pets", typeof(bool))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Custom exception indicating that api token is not valid or has not been reported", typeof(Exception))]
        public IHttpActionResult DeletePet(int id)
        {
            try
            {
                BancoPet.delete(id);
                return Content(HttpStatusCode.OK, true);
            }
            catch
            {
                return Content(HttpStatusCode.OK, false);
            }
        }
    }
}