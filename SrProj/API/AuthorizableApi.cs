using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using Microsoft.Ajax.Utilities;
using SrProj.API.Responses;
using SrProj.Models;
using SrProj.Models.Context;

namespace SrProj.API
{
    //TODO: Test!
    [AttributeUsage(AttributeTargets.Class)]
    public class AuthorizableController : AuthorizeAttribute
    {
        public new string[] Roles;
        public AuthorizableController(string[] roles = null)
        {
            this.Roles = roles ?? Role.authRoles;
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            return AuthorizationActions.Authorize(actionContext.Request, this.Roles);
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            ApiResponse response = new ApiResponse(actionContext.Request);
            //TODO: This error should be the same as the one in AuthorizableApi.
            response.errors.Add(new JsonError
            {
                id = 28
            });
            actionContext.Response = response.GenerateResponse(HttpStatusCode.Forbidden);
        }
    }
    
    [AttributeUsage(AttributeTargets.Method)]
    public class AuthorizableAction : AuthorizableController { }

    /*public class AuthorizableApi : ApiController
    {
        public string[] Roles;
        public AuthorizableApi(string[] roles = null)
        {
            this.Roles = roles ?? Role.authRoles;
        }

        public override Task<HttpResponseMessage> ExecuteAsync(HttpControllerContext controllerContext, CancellationToken cancellationToken)
        {
            /* Add functionality here to secure the API 
            var authorized = AuthorizationActions.Authorize(controllerContext.Request, this.Roles);
            if (!authorized)
            {
                ApiResponse response = new ApiResponse(Request);
                //TODO: Make this a real boy, I mean error.
                response.errors.Add(new JsonError
                {
                    id = 27
                });
                var res = new Task<HttpResponseMessage>(() => response.GenerateResponse(HttpStatusCode.Forbidden));
                res.Start();
                return res;
            }
            return base.ExecuteAsync(controllerContext, cancellationToken);
        }
    }*/

    internal static class AuthorizationActions
    {
        public static bool Authorize(HttpRequestMessage request, string[] roles)
        {
            string authToken = request.Headers.Single(h => h.Key == "authToken")
                .IfNotNull(kv => kv.Value.ToString());
            string activeUser = request.Headers.Single(h => h.Key == "username")
                .IfNotNull(kv => kv.Value.ToString());

            if (!string.IsNullOrEmpty(authToken) && !string.IsNullOrEmpty(activeUser))
            {
                var database = new Database();

                var session = database.AuthenticationTokens.Find(authToken);
                var authRoles = database.Roles.Where(r => roles.Contains(r.RoleName));

                if (session.LastAccessedTime <= DateTime.UtcNow.AddMinutes(-15) &&
                    session.AssociatedVolunteer.Username == activeUser &&
                    session.AssociatedVolunteer.Roles.Intersect(authRoles).Count() == authRoles.Count()
                    )
                {
                    return true;
                }
            }

            return false;
        }
    }
}