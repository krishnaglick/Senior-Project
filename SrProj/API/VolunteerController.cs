
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using SrProj.API.Responses;
using SrProj.Models;
using SrProj.Models.Context;
using PasswordHasher = SrProj.Utility.Security.PasswordHasher;

namespace SrProj.API
{
    public class VolunteerController : ApiController
    {
        [HttpPost]
        [AuthorizableAction]
        public HttpResponseMessage CreateVolunteer([FromBody] Volunteer volunteer)
        {
            ApiResponse response = new ApiResponse(Request);

            try
            {
                volunteer.CreateDate = DateTime.UtcNow;
                volunteer.SecurePassword();
                var volunteerContext = new Database();
                volunteerContext.Volunteers.Add(volunteer);
                volunteerContext.SaveChanges();

                response.data = response.DefaultSuccessResponse;
                return response.GenerateResponse(HttpStatusCode.Created);
            }
            catch (Exception e)
            {
                //TODO: Put this in its own error.
                response.errors.Add(new JsonError
                {
                    code = "Invalid Input",
                    detail = "There was something wrong with the provided information",
                    id = 1,
                    source = e
                });
                return response.GenerateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        public HttpResponseMessage Login([FromBody] Volunteer volunteer)
        {
            ApiResponse response = new ApiResponse(Request);
            var volunteerContext = new Database();
            var foundVolunteer = volunteerContext.Volunteers.Find(volunteer.Username);

            if (foundVolunteer == null)
            {
                //TODO: Put this in its own error.
                response.errors.Add(new JsonError
                {
                    code = "Invalid Username or Password",
                    detail = "Your username or password was incorrect!",
                    id = 2
                });
                return response.GenerateResponse(HttpStatusCode.BadRequest);
            }

            var passwordResult = foundVolunteer.VerifyPassword(volunteer.Password);
            if(passwordResult == PasswordVerificationResult.SuccessRehashNeeded)
            {
                foundVolunteer.Password = PasswordHasher.EncryptPassword(volunteer.Password);
                passwordResult = PasswordVerificationResult.Success;
            }
            if (passwordResult == PasswordVerificationResult.Success)
            {
                var authTokenID = Guid.NewGuid();
                var authToken = new AuthenticationToken
                {
                    Token = authTokenID,
                    LastAccessedTime = DateTime.UtcNow,
                    AssociatedVolunteer = foundVolunteer
                };
                var authTokenContext = volunteerContext;
                authTokenContext.AuthenticationTokens.Add(authToken);
                authTokenContext.SaveChanges();

                return response.GenerateResponse(HttpStatusCode.OK, new Dictionary<string, string>
                {
                    {"authToken", authTokenID.ToString()}
                });
            }
            else
            {
                response.errors.Add(new JsonError
                {
                    code = "Invalid Username or Password",
                    detail = "Your username or password was incorrect!",
                    id = 2
                });
                return response.GenerateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}
