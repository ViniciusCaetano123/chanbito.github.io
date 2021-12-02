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
    public class OngController : ApiController
    {
        [HttpGet]
        [AllowAnonymous]
        [SwaggerResponse(HttpStatusCode.OK, "Return a list of pets", typeof(List<OngClass>))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Custom exception indicating that api token is not valid or has not been reported", typeof(Exception))]
        public IHttpActionResult GetAllOngs()
        {
            return Content(HttpStatusCode.OK, BancoOng.selectAll());
        }

        [HttpPost]
        [AllowAnonymous]
        [SwaggerResponse(HttpStatusCode.OK, "Return the id from this pet", typeof(int))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Custom exception indicating that api token is not valid or has not been reported", typeof(Exception))]
        public IHttpActionResult InsertOng(OngClass ong)
        {
            return Content(HttpStatusCode.OK, BancoOng.insert(ong));
        }
        /// <summary>
        /// adiciona um pet a uma ong
        /// </summary>
        /// <param name="IDong">ID da Ong</param>
        /// <param name="IDPet">ID do PET</param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, type:typeof(bool))]
        [AllowAnonymous]
        public IHttpActionResult ReceberPet(int IDong, int IDPet)
        {
            try
            {
                return Content(System.Net.HttpStatusCode.OK, BancoOng.relacionar(IDong, IDPet));
            }catch (Exception e)
            {
                return Content(System.Net.HttpStatusCode.InternalServerError, hel e.Message);
            }

        }
    }
}