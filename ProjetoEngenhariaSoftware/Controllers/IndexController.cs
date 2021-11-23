using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.Swagger.Annotations;

namespace ProjetoEngenhariaSoftware.Controllers
{
    public class IndexController : ApiController
    {
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, "retorno", typeof(string))]
        public IHttpActionResult Get()
        {
            return Content(HttpStatusCode.OK, "teste");
        }
    }
}