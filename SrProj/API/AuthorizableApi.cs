
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using SrProj.API.Responses;
using SrProj.API.Responses.Errors;
using Utility.Enum;
using Utility.ExtensionMethod;
using Database = DataAccess.Contexts.Database;

namespace SrProj.API
{
    internal sealed class AuthorizationOptions
    {
        public static readonly RoleID[] DefaultAuthRoles = { RoleID.Admin };
        public static readonly int AuthTokenTimeout = 15; //In Minutes
    }

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
            base.OnAuthorization(actionContext);
            return;
            var authResult = AuthorizationActions.Authorize(actionContext.Request, this.DefaultAuthRoles);
            if (authResult != AuthorizationResult.Success)
            {
                ApiResponse response = new ApiResponse(actionContext.Request);
                if (authResult == AuthorizationResult.MismatchedUser || authResult == AuthorizationResult.ExpiredToken ||
                    authResult == AuthorizationResult.InvalidRequest)
                {
                    response.errors.Add(new InvalidToken());
                }
                else if (authResult == AuthorizationResult.Unauthorized)
                {
                    response.errors.Add(new NoAccess());
                }

                actionContext.Response = response.GenerateResponse(HttpStatusCode.Forbidden);
            }

            base.OnAuthorization(actionContext);
        }
    }
    
    [AttributeUsage(AttributeTargets.Method)]
    public class AuthorizableAction : AuthorizableController { }

    public enum AuthorizationResult
    {
        Success = 0,

        MismatchedUser = 1,

        Unauthorized = 2,

        ExpiredToken = 3,

        InvalidRequest = 4
    }

    internal static class AuthorizationActions
    {
        public static AuthorizationResult Authorize(HttpRequestMessage request, RoleID[] roles)
        {
            string authToken = request.Headers.GetHeaderValue("authToken");
            string activeUser = request.Headers.GetHeaderValue("username");

            AuthorizationResult? authResult = null;

            if (!string.IsNullOrEmpty(authToken) && !string.IsNullOrEmpty(activeUser))
            {
                using (var database = new Database())
                {
                    var session =
                        database.AuthenticationTokens.Include(at => at.AssociatedVolunteer)
                            .Include(at => at.AssociatedVolunteer.Roles)
                            .FirstOrDefault(at => at.Token.ToString() == authToken);

                    if (session == null) return AuthorizationResult.ExpiredToken;

                    int[] roleIDs = roles.Select(r => (int)r).ToArray();
                    var lastAccessedTime = session.LastAccessedTime;
                    //I have to do this so the auth token gets updated in the DB. Probably worth switching up what I'm doing here.

                    var matchingRoles = session.AssociatedVolunteer.Roles.Where(r => roleIDs.Contains(r.Role.ID)).ToList();

                    if (session.AssociatedVolunteer.Username != activeUser)
                    {
                        authResult = AuthorizationResult.MismatchedUser;
                    }
                    else if (matchingRoles.Count != roles.Length)
                    {
                        authResult = AuthorizationResult.Unauthorized;
                    }
                    //TODO: This needs fixing and testing.
                    else if (lastAccessedTime > DateTime.UtcNow.AddMinutes(AuthorizationOptions.AuthTokenTimeout) &&
                             lastAccessedTime < DateTime.UtcNow.AddSeconds(20))
                    {
                        database.AuthenticationTokens.Remove(session);
                        authResult = AuthorizationResult.ExpiredToken;
                    }
                    else
                    {
                        authResult = AuthorizationResult.Success;
                    }

                    database.SaveChanges();
                }
            }

            return authResult ?? AuthorizationResult.InvalidRequest;
        }
    }
}