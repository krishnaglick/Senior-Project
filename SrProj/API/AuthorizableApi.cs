
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using SrProj.API.Responses;
using SrProj.Models;
using SrProj.Utility.ExtensionMethod;
using Database = SrProj.Models.Context.Database;

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

            base.OnAuthorization(actionContext);
        }
    }
    
    [AttributeUsage(AttributeTargets.Method)]
    public class AuthorizableAction : AuthorizableController { }

    internal static class AuthorizationActions
    {
        public static bool Authorize(HttpRequestMessage request, RoleID[] roles)
        {
            string authToken = request.Headers.GetHeaderValue("authToken");
            string activeUser = request.Headers.GetHeaderValue("username");

            if (!string.IsNullOrEmpty(authToken) && !string.IsNullOrEmpty(activeUser))
            {
                var database = new Database();
                //var session = database.AuthenticationTokens.Include(at => at.).Find(Guid.Parse(authToken));
                var session =
                    database.AuthenticationTokens.Include(at => at.AssociatedVolunteer).Include(at => at.AssociatedVolunteer.Roles)
                        .FirstOrDefault(at => at.Token.ToString() == authToken) ?? new AuthenticationToken { CreateDate = DateTime.UtcNow.AddMinutes(-60)};

                int[] roleIDs = roles.Select(r => (int) r).ToArray();
                var lastAccessedTime = session.LastAccessedTime;
                //I have to do this so the auth token gets updated in the DB. Probably worth switching up what I'm doing here.
                database.SaveChanges();

                var matchingRoles = session.AssociatedVolunteer.Roles.Where(r => roleIDs.Contains(r.ID)).ToList();

                if (lastAccessedTime > DateTime.UtcNow.AddMinutes(-AuthorizationOptions.AuthTokenTimeout) &&
                    lastAccessedTime < DateTime.UtcNow.AddSeconds(20) &&
                    session.AssociatedVolunteer.Username == activeUser &&
                    matchingRoles.Count == roles.Length
                    )
                {
                    return true;
                }
            }

            return false;
        }
    }
}