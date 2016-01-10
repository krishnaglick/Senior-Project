
using System;
using System.Collections.Generic;
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

                string activeUser = actionContext.Request.Headers.GetHeaderValue("username");
                actionContext.Response = response.GenerateResponse(HttpStatusCode.Forbidden, new Dictionary<string, string>
                {
                    { "authToken", Authorization.GenerateToken(activeUser) }
                });
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

        InvalidRequest = 4,

        InvalidToken = 5
    }

    internal static class AuthorizationActions
    {
        public static AuthorizationResult Authorize(HttpRequestMessage request, RoleID[] roles)
        {
            string authToken = request.Headers.GetHeaderValue("authToken");
            string activeUser = request.Headers.GetHeaderValue("username");

            if (!string.IsNullOrEmpty(authToken) && !string.IsNullOrEmpty(activeUser))
            {
                var database = new Database();
                var decodedAuthToken = Authorization.DecodeToken(authToken);
                if (decodedAuthToken == null)
                    return AuthorizationResult.InvalidToken;

                if(decodedAuthToken.username != activeUser)
                    return AuthorizationResult.MismatchedUser;

                if(decodedAuthToken.timeDiff > AuthorizationOptions.AuthTokenTimeout)
                    return AuthorizationResult.ExpiredToken;

                //Valid token, need to check roles
                var dbRoles = database.RoleVolunteers.Where(rv => rv.Volunteer.Username == activeUser)
                    .Select(rv => rv.Role.ID).ToArray();

                if(roles.Select(r => (int) r).Intersect(dbRoles).Count() == roles.Length)
                    return AuthorizationResult.Success;
                    
                return AuthorizationResult.Unauthorized;
            }

            return AuthorizationResult.InvalidRequest;
        }
    }
}