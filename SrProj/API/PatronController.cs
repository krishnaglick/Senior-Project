
using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Models;
using SrProj.API.Responses;
using SrProj.API.Responses.Errors;
using Utility.Enum;
using Database = DataAccess.Contexts.Database;

namespace SrProj.API
{
    [AuthorizableController(new [] { RoleID.Volunteer })]
    public class PatronController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage FindPatron(dynamic data)
        {
            //Fuck C#
            Patron searchData = new Patron
            {
                FirstName = data.firstName,
                MiddleName = data.middleName,
                LastName = data.lastName,
                DateOfBirth = data.dateOfBirth.HasValues ? data.dateOfBirth : DateTime.MinValue
            };
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

        public class CheckInViewModel : Patron
        {
            //TODO: Populate this based off of camel-cased visit information. God damnit C#.
        }

        [HttpPost]
        public HttpResponseMessage CheckIn(dynamic visit)
        {
            ApiResponse response = new ApiResponse(Request);
            if (visit == null)
            {
                response.errors.Add(new NullRequest());
                return response.GenerateResponse(HttpStatusCode.BadRequest);
            }
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
