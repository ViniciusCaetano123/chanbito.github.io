using Microsoft.AspNet.WebApi.Extensions.Compression.Server;
using System;
using System.Configuration;
using System.Net.Http.Extensions.Compression.Core.Compressors;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ProjetoEngenhariaSoftware
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            //Rota padrão
            string defaultRoutePrefix = String.Format("api/v{0}/", ConfigurationManager.AppSettings["api_version"]);
            defaultRoutePrefix += "{controller}/{action}/{id}";

            var corsAttr = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(corsAttr);

            config.Routes.MapHttpRoute(
                name: "ProjetoEngenhariaSoftware",
                routeTemplate: defaultRoutePrefix,
                defaults: new { id = RouteParameter.Optional }
            );
           
            config.Filters.Add(new AuthorizeFilter());

			GlobalConfiguration.Configuration.MessageHandlers.Insert(0, new ServerCompressionHandler(new GZipCompressor(), new DeflateCompressor()));
        }
    }
}