﻿
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
using Database = DataAccess.Contexts.Database;

namespace SrProj.API
{
    public class VolunteerController : ApiController
    {
        [HttpPost]
        //[AuthorizableAction]
        public HttpResponseMessage CreateVolunteer([FromBody] Volunteer volunteer)
        {
            ApiResponse response = new ApiResponse(Request);

            try
            {
                volunteer.SecurePassword();
                using (var volunteerContext = new Database())
                {
                    volunteerContext.Volunteers.Add(volunteer);
                    volunteerContext.SaveChanges();

                    response.data = response.DefaultSuccessResponse;
                    return response.GenerateResponse(HttpStatusCode.Created);
                }
            }
            catch (Exception e)
            {
                response.errors.Add(new InvalidVolunteer { source = e });
                return response.GenerateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        public HttpResponseMessage Login([FromBody] Login volunteer)
        {
            ApiResponse response = new ApiResponse(Request);
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

                response.data = new {roles = foundVolunteer.Roles};

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
    }
}
