using ProjetoEngenhariaSoftware.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Security.Authentication;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace ProjetoEngenhariaSoftware
{
    public sealed class AuthorizeFilter : AuthorizeAttribute
    {
        //
        // Summary:
        //     Calls when an action is being authorized.
        //
        // Parameters:
        //   actionContext:
        //     The context.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     The context parameter is null.
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //base.OnAuthorization(actionContext);

            //TODO Verificar se têm token, se não têm retornar que não têm permissão para acesso ao método, verificar também grupos(ROLES)
            if (IsAuthorizedToken(actionContext)==0)
            {
                actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized, new InvalidCredentialException());
            }
            else if(IsAuthorizedToken(actionContext) == 2){
                actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized, new TimeoutException());
            }
        }

        //
        // Summary:
        //     Processes requests that fail authorization.
        //
        // Parameters:
        //   actionContext:
        //     The context.
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {

        }

        //
        // Summary:
        //     Indicates whether the specified control is authorized.
        //
        // Parameters:
        //   actionContext:
        //     The context.
        //
        // Returns:
        //     true if the control is authorized; otherwise, false.
        internal int IsAuthorizedToken(HttpActionContext actionContext)
        {
            //base.IsAuthorized(actionContext);
            if (!SkipAuthorization(actionContext)) //if action has Annotation @AllowAnonymous
            {
                if (actionContext.Request.Headers.Authorization != null) //If Request has Authorization header
                    if (!String.IsNullOrEmpty(actionContext.Request.Headers.Authorization.Scheme)) //if authorization scheme are set.
                        if (actionContext.Request.Headers.Authorization.Scheme.Equals(ConfigurationManager.AppSettings["AuthorizationScheme"]))//if authorization scheme are equals system scheme.
                return JWTHelper.validate(actionContext.Request.Headers.Authorization.Parameter); //IF token is valid (same server, expiration date is oldest then datetime.now, verified signature, etc...)
                            
            }
            else
            {
                return 1;
            }

            return 0;
        }

        protected override bool IsAuthorized(HttpActionContext actionContext){
            return true;
        }

        private static bool SkipAuthorization(HttpActionContext actionContext)
        {
            if (!Enumerable.Any<AllowAnonymousAttribute>((IEnumerable<AllowAnonymousAttribute>)
                actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>()))
                return Enumerable.Any<AllowAnonymousAttribute>((IEnumerable<AllowAnonymousAttribute>)
                    actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>());
            else
                return true;
        }
    }
}