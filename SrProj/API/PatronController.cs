﻿
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAccess.Contexts;
using Models;
using SrProj.API.Responses;
using SrProj.API.Responses.Errors;
using Utility.Enum;

namespace SrProj.API
{
    [AuthorizableController(new [] { RoleID.Volunteer })]
    public class PatronController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage FindPatron([FromBody] Patron searchData)
        {
            ApiResponse response = new ApiResponse(Request);
            try
            {
                var patronContext = new Database();
                var patrons = patronContext.Patrons.Where(
                    p =>
                        p.FirstName.ToLower().Contains(searchData.FirstName.ToLower()) ||
                        p.MiddleName.ToLower().Contains(searchData.MiddleName.ToLower()) ||
                        p.LastName.ToLower().Contains(searchData.LastName.ToLower()) ||
                        p.DateOfBirth.ToString().Contains(searchData.DateOfBirth.ToString()));

                response.data = patrons;
                return response.GenerateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                response.errors.Add(new InvalidPatron { source = e });
                return response.GenerateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        public HttpResponseMessage Create(Patron patron)
        {
            ApiResponse response = new ApiResponse(Request);
            try
            {
                var patronContext = new Database();
                patronContext.Patrons.Add(patron);
                patronContext.SaveChanges();

                response.data = ApiResponse.DefaultSuccessResponse;
                return response.GenerateResponse(HttpStatusCode.Created);
            }
            catch(Exception e)
            {
                response.errors.Add(new InvalidPatron { source = e });
                return response.GenerateResponse(HttpStatusCode.BadRequest);
            }
        }

        public class CheckInViewModel : Patron
        {

        }

        [HttpPost]
        public HttpResponseMessage CheckIn(dynamic visit)
        {
            ApiResponse response = new ApiResponse(Request);
            try
            {
                var checkInContext = new Database();
                return response.GenerateResponse(HttpStatusCode.Created);
            }
            catch(Exception e)
            {
                response.errors.Add(new InvalidCheckIn { source = e });
                return response.GenerateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}
