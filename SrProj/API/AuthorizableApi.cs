
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Microsoft.Ajax.Utilities;
using SrProj.API.Responses;
using SrProj.Models;
using SrProj.Models.Context;

namespace SrProj.API
{
    internal sealed class AuthorizationOptions
    {
        public static readonly RoleID[] DefaultAuthRoles = { RoleID.Admin };
        public static readonly int AuthTokenTimeout = 15; //In Minutes
    }

    //TODO: Test!
    [AttributeUsage(AttributeTargets.Class)]
    public class AuthorizableController : AuthorizationFilterAttribute
    {
        public RoleID[] DefaultAuthRoles;
        public AuthorizableController(RoleID[] roles = null)
        {
            this.DefaultAuthRoles = roles ?? AuthorizationOptions.DefaultAuthRoles;
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var isAuthorized = AuthorizationActions.Authorize(actionContext.Request, this.DefaultAuthRoles);
            if(!isAuthorized)
            {
                ApiResponse response = new ApiResponse(actionContext.Request);
                //TODO: Put this error in its own error file.
                response.errors.Add(new JsonError
                {
                    id = 28
                });
                actionContext.Response = response.GenerateResponse(HttpStatusCode.Forbidden);
            }
        }
    }
    
    [AttributeUsage(AttributeTargets.Method)]
    public class AuthorizableAction : AuthorizableController { }

    internal static class AuthorizationActions
    {
        public static bool Authorize(HttpRequestMessage request, RoleID[] roles)
        {
            //TODO: Actually get header values, this doesn't work. T.T
            string authToken = request.Headers.Single(h => h.Key == "authToken")
                .IfNotNull(kv => kv.Value.ToString()); 
            string activeUser = request.Headers.Single(h => h.Key == "username")
                .IfNotNull(kv => kv.Value.ToString());

            if (!string.IsNullOrEmpty(authToken) && !string.IsNullOrEmpty(activeUser))
            {
                var session = new Database().AuthenticationTokens.Find(authToken);

                int[] roleIDs = roles.Select(r => (int) r).ToArray();

                //TODO: Confirm that <= is the right thing to use here.
                if (session.LastAccessedTime <= DateTime.UtcNow.AddMinutes(-AuthorizationOptions.AuthTokenTimeout) &&
                    session.AssociatedVolunteer.Username == activeUser &&
                    session.AssociatedVolunteer.Roles.Count(r => roleIDs.Contains(r.ID)) == roles.Length
                    )
                {
                    return true;
                }
            }

            return false;
        }
    }
}