
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Models;
using SrProj.API.Responses;
using SrProj.API.Responses.Errors;
using Utility.ExtensionMethod;
using Database = DataAccess.Contexts.Database;

namespace SrProj.API
{
    public class LoginController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Login([FromBody] Login volunteer)
        {
            ApiResponse response = new ApiResponse(Request);
            using (var dbContext = new Database())
            {
                var roleVolunteer = dbContext.RoleVolunteers
                    .Include(rv => rv.Volunteer)
                    .Include(rv => rv.Role)
                    .Where(rv => rv.Volunteer.Username == volunteer.Username)
                    .ToList();

                if (roleVolunteer.Count == 0)
                {
                    response.errors.Add(new InvalidUsernameOrPassword());
                    return response.GenerateResponse(HttpStatusCode.BadRequest);
                }

                var dbVolunteer = roleVolunteer[0].Volunteer;
                var roles = roleVolunteer.Select(rv => rv.Role.ID).Distinct().ToArray();

                var passwordResult = dbVolunteer.VerifyPassword(volunteer.Password);
                if (passwordResult == PasswordVerificationResult.SuccessRehashNeeded)
                {
                    dbVolunteer.Password = Volunteer.hasher.HashPassword(volunteer.Password);
                    passwordResult = PasswordVerificationResult.Success;
                }
                if (passwordResult == PasswordVerificationResult.Success)
                {
                    /*var authTokenID = Guid.NewGuid();
                    var authToken = new AuthenticationToken
                    {
                        Token = authTokenID,
                        AssociatedVolunteer = dbVolunteer
                    };
                    var authTokenContext = dbContext;
                    authTokenContext.AuthenticationTokens.Add(authToken);
                    authTokenContext.SaveChanges();*/
                    var authToken = Authorization.GenerateToken(volunteer.Username);

                    response.data = new {roles = roleVolunteer.Select(rv => rv.Role.RoleName)};

                    return response.GenerateResponse(HttpStatusCode.OK, new Dictionary<string, string>
                    {
                        { "authToken", authToken }
                    });
                }
                else
                {
                    response.errors.Add(new InvalidUsernameOrPassword());
                    return response.GenerateResponse(HttpStatusCode.BadRequest);
                }
            }
        }

        [HttpGet]
        public void Logout()
        {
            Guid authToken = Guid.Parse(Request.Headers.GetHeaderValue("authToken") ?? Guid.Empty.ToString());
            if (authToken == Guid.Empty) return;

            using (var db = new Database())
            {
                var tokenToRemove = db.AuthenticationTokens.FirstOrDefault(t => t.Token.Equals(authToken));
                if (tokenToRemove == null) return;
                db.AuthenticationTokens.Remove(tokenToRemove);
            }
        }
    }
}