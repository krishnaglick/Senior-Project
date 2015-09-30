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
    //TODO: Test, and it can probably use a refactor.
    [AttributeUsage(AttributeTargets.Method)]
    public class AuthorizableRoute : AuthorizeAttribute
    {
        public new List<Role> Roles;
        public AuthorizableRoute(List<Role> roles = null)
        {
            this.Roles = roles ?? Role.GetAuthorizedRoles();
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

    public class AuthorizableApi : ApiController
    {
        public List<Role> Roles;
        public AuthorizableApi(List<Role> roles = null)
        {
            this.Roles = roles ?? Role.GetAuthorizedRoles();
        }

        public override Task<HttpResponseMessage> ExecuteAsync(HttpControllerContext controllerContext, CancellationToken cancellationToken)
        {
            /* Add functionality here to secure the API */
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
    }

    internal static class AuthorizationActions
    {
        public static bool Authorize(HttpRequestMessage request, List<Role> roles)
        {
            string authToken = request.Headers.Single(h => h.Key == "authToken")
                .IfNotNull(kv => kv.Value.ToString());
            string activeUser = request.Headers.Single(h => h.Key == "username")
                .IfNotNull(kv => kv.Value.ToString());

            if (!string.IsNullOrEmpty(authToken) && !string.IsNullOrEmpty(activeUser))
            {
                var session = new Database().AuthenticationTokens.Find(authToken);

                if (session.LastAccessedTime <= DateTime.UtcNow.AddMinutes(-15) &&
                    session.AssociatedVolunteer.Username == activeUser &&
                    session.AssociatedVolunteer.Roles.Intersect(roles).Count() == roles.Count
                    )
                {
                    return true;
                }
            }

            return false;
        }
    }
}