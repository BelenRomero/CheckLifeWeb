using CheckLifeWeb.SessionHelpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Linq;

namespace CheckLifeWeb.Controllers
{
    public class CustomAuthorizationFilterAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any()) return;


            var  usuario = new SessionHelper().GetUsuario(context.HttpContext.Session);

            if (usuario != null)
            {
                
            }
            else
            {
                context.Result = new RedirectToRouteResult
                (
                new RouteValueDictionary(new
                {
                    action = "Error",
                    controller = "Error"
                }));

            }
        }
    }
}