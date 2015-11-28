
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
            //TODO: using
            var volunteerContext = new Database();
            var foundVolunteer = volunteerContext.Volunteers.Include(v => v.Roles).FirstOrDefault(v => v.Username == volunteer.Username);

            if (foundVolunteer == null)
            {
                response.errors.Add(new InvalidUsernameOrPassword());
                return response.GenerateResponse(HttpStatusCode.BadRequest);
            }

            var passwordResult = foundVolunteer.VerifyPassword(volunteer.Password);
            if(passwordResult == PasswordVerificationResult.SuccessRehashNeeded)
            {
                foundVolunteer.Password = Volunteer.hasher.HashPassword(volunteer.Password);
                passwordResult = PasswordVerificationResult.Success;
            }
            if (passwordResult == PasswordVerificationResult.Success)
            {
                var authTokenID = Guid.NewGuid();
                var authToken = new AuthenticationToken
                {
                    Token = authTokenID,
                    AssociatedVolunteer = foundVolunteer
                };
                var authTokenContext = volunteerContext;
                authTokenContext.AuthenticationTokens.Add(authToken);
                authTokenContext.SaveChanges();

                response.data = new {roles = foundVolunteer.Roles.Select(r => r.RoleName)};

                return response.GenerateResponse(HttpStatusCode.OK, new Dictionary<string, string>
                {
                    {"authToken", authTokenID.ToString()}
                });
            }
            else
            {
                response.errors.Add(new InvalidUsernameOrPassword());
                return response.GenerateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        public void Logout()
        {
            Guid authToken = Guid.Parse(Request.Headers.GetHeaderValue("authToken") ?? Guid.Empty.ToString());
            //TODO: using
            var db = new Database();
            var tokenToRemove = db.AuthenticationTokens.FirstOrDefault(t => t.Token.Equals(authToken));
            if (tokenToRemove == null) return;
            db.AuthenticationTokens.Remove(tokenToRemove);
        }
    }
}