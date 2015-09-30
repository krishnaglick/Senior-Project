using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Security;

namespace SrProj.API
{
    //TODO: Make this work.
    [AttributeUsage(AttributeTargets.Method)]
    public class AuthorizableRoute : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            // do any stuff here
            // it will be invoked when the decorated method is called
            if (true)
                return true; // authorized
            else
                return false; // not authorized
        }
    }

    public class AuthorizableApi : ApiController
    {
        public override Task<HttpResponseMessage> ExecuteAsync(HttpControllerContext controllerContext, CancellationToken cancellationToken)
        {
            /* Add functionality here to secure the API */
            //TODO: Need to use TryGetValue.
            /*var authToken = new Database().AuthenticationTokens.Find(controllerContext.Request.Headers.GetValues("authToken"));
            if (authToken == null)
            {
                var response = new ApiResponse(controllerContext.Request);
                response.errors.Add(new JsonError{code = "Bad!"});

                response.GenerateResponse(HttpStatusCode.Unauthorized);
            }*/
            //Attribute.GetCustomAttribute(myRoute, typeof (AuthorizableRoute));
            return base.ExecuteAsync(controllerContext, cancellationToken);
        }
    }

    internal static class AuthorizationActions
    {
        public static bool Authorize(HttpControllerContext controllerContext, IEnumerable<Role> roles)
        {
            
        }
    }
}